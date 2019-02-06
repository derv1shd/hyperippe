using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Hyperippe.GraphModel;

namespace Hyperippe.Workers
{
    /// <summary>
    /// The Pruner class is tasked with deciding if a particular node, link or content is of interest.
    /// It contains method to compare nodes, evaluate the content of a node (i.e. HTML anchors), and decide
    /// if a link should be pursued. It is called by the Spider class whenever one of these conditions arise.
    /// </summary>
    public class Pruner
    {
        private ICrawlRecorder myCrawlListener;
        private List<Uri> myTargets;
        private Regex anchorsRegex;
        private List<Uri> additionalTargets;
        private int additionalTargetsAvailable;

        public Pruner(List<Uri> targets, ICrawlRecorder recorder, int additionalTargetCount)
        {
            anchorsRegex = new Regex("(?i)<a([^>]+)>(.+?)</a>");
            myCrawlListener = recorder;
            myTargets = targets;
            additionalTargets = new List<Uri>();
            additionalTargetsAvailable = additionalTargetCount;
            List<Uri> extraSchemes = new List<Uri>();
            foreach (Uri target in myTargets)
            {
                Uri uri = null;
                if (target.ToString().StartsWith("http://"))
                {
                    uri = new Uri(target.ToString().Replace("http://", "https://"));
                }
                if (target.ToString().StartsWith("https://"))
                {
                    uri = new Uri(target.ToString().Replace("https://", "http://"));
                }
                if(uri != null)
                    extraSchemes.Add(uri);
            }
            myTargets.AddRange(extraSchemes);
        }

        public bool ShouldPursue(Uri uri)
        {
            foreach (Uri target in myTargets)
            {
                if (target == uri || target.IsBaseOf(uri))
                {
                    if (additionalTargetsAvailable > 0)
                    {
                        additionalTargetsAvailable--;
                        additionalTargets.Add(uri);
                        return true;
                    }
                    else
                        return false;
                }
            }
            return false;
        }

        public bool ShouldPursue(Link link)
        {
            foreach (Uri target in myTargets)
            {
                if (target.IsBaseOf(link.Uri))
                {
                    if (additionalTargetsAvailable > 0)
                    {
                        additionalTargetsAvailable--;
                        additionalTargets.Add(link.Uri);
                        return true;
                    }
                    else
                        return false;
                }
            }
            return false;
        }

        public int Compare(NodeContent nodeContent, string current)
        {
            if (nodeContent.Content != current)
                return -1;
            else
                return 0;
        }

        public int Compare(NodeContent nodeContent, List<Link> currentLinks)
        {
            if (nodeContent.Links.Count != currentLinks.Count)
                return -1;
            else
                return 0;
        }

        public List<Link> EvalLinks(NodeContent nodeContent)
        {
            List<Link> results = new List<Link>();

            if (!nodeContent.ContentType.StartsWith("text/html")) return results;

            try
            {
                foreach (Match anchor in anchorsRegex.Matches(nodeContent.Content))
                {
                    string url = anchor.Value;
                    // TODO: this should be done not only with double quotes
                    if (!url.Contains("href=\""))
                        continue;
                    url = url.Substring(url.IndexOf("href=\"") + 6);
                    if (url.Contains("\""))
                        url = url.Substring(0, url.IndexOf("\""));
                    if (url.Contains("#"))
                        url = url.Substring(0, url.IndexOf("#"));
                    Uri uri = null;
                    try
                    {
                        uri = new Uri(url);
                    }
                    catch (UriFormatException)
                    {
                        // Try to construct a full url, in case what we have is a relative url
                        url = nodeContent.Node.Uri.ToString() + "/" + url;
                        // Remove excess slashes
                        url = url.Replace("///", "/");
                        while (url.Substring(url.IndexOf("://")+3).Contains("//"))
                        {
                            url = url.Substring(0, url.IndexOf("://") + 3) + url.Substring(url.IndexOf("://") + 3).Replace("//", "/");
                        }
                        try
                        {
                            uri = new Uri(url);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        myCrawlListener.ExceptionRaised(this, ex);
                    }
                    if (uri != null)
                        results.Add(new Link(uri));
                }
            }
            catch(Exception ex)
            {
                myCrawlListener.ExceptionRaised(this, ex);
            }
            return results;
        }
    }
}
