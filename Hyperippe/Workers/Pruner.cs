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
        private Regex anchors;
        private Regex hrefs;

        public Pruner(List<Uri> targets, ICrawlRecorder recorder)
        {
            anchors = new Regex("(?i)<a([^>]+)>(.+?)</a>");
            hrefs = new Regex("\\s*(?i)href\\s*=\\s*(\"([^\"]*\")|'[^']*'|([^'\">\\s]+))");
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
                foreach (Match anchor in anchors.Matches(nodeContent.Content))
                {
                    foreach (Match href in hrefs.Matches(anchor.ToString()))
                    {
                        string url = href.ToString();
                        url = url.Substring(url.IndexOf('"') + 1);
                        url = url.Substring(0, url.LastIndexOf('"'));
                        Uri uri = null;
                        try
                        {
                            uri = new Uri(url);
                        }
                        catch (UriFormatException)
                        {
                            // Try to construct a full url, in case what we hace is a relative url
                            url = nodeContent.Node.Uri.ToString() + "/" + url;
                            // Remove excess slashes
                            url = url.Replace("///", "/");
                            url = url.Replace("//", "/");
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
            }
            catch(Exception ex)
            {
                myCrawlListener.ExceptionRaised(this, ex);
            }
            return results;
        }
    }
}
