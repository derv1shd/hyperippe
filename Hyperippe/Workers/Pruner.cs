using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Hyperippe.GraphModel;

namespace Hyperippe.Workers
{
    public class Pruner
    {
        private ICrawlListener myCrawlListener;
        private Regex anchors;
        private Regex hrefs;

        public Pruner(ICrawlListener crawlListener)
        {
            anchors = new Regex("(?i)<a([^>]+)>(.+?)</a>");
            hrefs = new Regex("\\s*(?i)href\\s*=\\s*(\"([^\"]*\")|'[^']*'|([^'\">\\s]+))");
            myCrawlListener = crawlListener;
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
