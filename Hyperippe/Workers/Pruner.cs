using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Hyperippe.GraphModel;

namespace Hyperippe.Workers
{
    public class Pruner
    {
        private ICrawlRecorder myCrawlListener;
        private List<Uri> myTargets;
        private Regex anchorsRegex;

        public Pruner(List<Uri> targets, ICrawlRecorder recorder)
        {
            anchorsRegex = new Regex("(?i)<a([^>]+)>(.+?)</a>");
            myCrawlListener = recorder;
            myTargets = targets;
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

        public bool ShouldPursue(Link link)
        {
            foreach(Uri target in myTargets){
                if (target.IsBaseOf(link.Uri))
                    return true;
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
            if (nodeContent.Node.Links.Count != currentLinks.Count)
                return -1;
            else
                return 0;
        }

        public List<Link> EvalLinks(NodeContent nodeContent)
        {
            List<Link> results = new List<Link>();

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
