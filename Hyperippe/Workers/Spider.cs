using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Hyperippe.GraphModel;

namespace Hyperippe.Workers
{
    public class Spider
    {
        private Baseline myBaseline;
        private Pruner myPruner;
        private ICrawlRecorder myCrawlListener;
        private long sessionId;
        private string userAgent { get => "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36";  }

        public Spider(Baseline baseline, Pruner pruner, ICrawlRecorder crawlerReport)
        {
            myPruner = pruner;
            myCrawlListener = crawlerReport ?? throw new ArgumentNullException(nameof(crawlerReport));
            sessionId = myCrawlListener.CrawlSessionBegin();
            int beatId = myCrawlListener.CrawlBeatBegin(sessionId);

            myBaseline = baseline ?? throw new ArgumentNullException(nameof(baseline));

            Dictionary<string, Node> extraNodes = new Dictionary<string, Node>();
            foreach (var valuePair in myBaseline)
            {
                NodeContent nodeContent = valuePair.Value;
                HttpStatusCode status;
                string current = ReadUri(nodeContent.Node.Uri, out status);
                nodeContent.Update(current);
                nodeContent.Node.Links = myPruner.EvalLinks(nodeContent);
                myCrawlListener.NodeRegistered(beatId, nodeContent, ((int)status).ToString());
                foreach(Link link in nodeContent.Node.Links)
                {
                    Node node = new Node(link.Uri.ToString());
                    if (!myBaseline.ContainsKey(node.Key) && !extraNodes.ContainsKey(node.Key))
                    {
                        if (myPruner.ShouldPursue(node.Uri))
                        {
                            extraNodes.Add(node.Key, node);
                        }
                    }
                }
            }
            myCrawlListener.MessageLogged("Adding " + extraNodes.Count.ToString() + " node(s) from first evaluation of links");
            foreach(var extra in extraNodes)
            {
                NodeContent extraNodeContent = new NodeContent(extra.Value, string.Empty);
                myBaseline.Add(extraNodeContent.Node.Key, extraNodeContent);
                myCrawlListener.NodeRegistered(beatId, extraNodeContent, ((int)HttpStatusCode.NoContent).ToString());
            }
            myCrawlListener.CrawlBeatEnd(beatId);
        }

        public bool Crawl()
        {
            int beatId = myCrawlListener.CrawlBeatBegin(sessionId);
            Dictionary<string, Node> newNodes = new Dictionary<string, Node>();

            foreach (var valuePair in myBaseline)
            {
                NodeContent nodeContent = valuePair.Value;
                HttpStatusCode status;
                string current = ReadUri(nodeContent.Node.Uri, out status);

                if(status != HttpStatusCode.OK)
                {
                    myCrawlListener.NodeStatusReported(beatId, nodeContent, ((int)status).ToString());
                }
                else if(myPruner.Compare(nodeContent, current) != 0)
                {
                    if (nodeContent.Content.Length > 0)
                    {
                        //Only notify a change if previous content wasn't zero
                        myCrawlListener.NodeChangeDetected(beatId, nodeContent, current, ((int)status).ToString());
                    }
                    nodeContent.Update(current);
                    List<Link> newLinks = myPruner.EvalLinks(nodeContent);
                    if (myPruner.Compare(nodeContent, newLinks) != 0)
                    {
                        if (nodeContent.Node.Links.Count > 0)
                        {
                            //Only notify a change if previous links weren't empty
                            myCrawlListener.NodeLinkChangeDetected(beatId, nodeContent, newLinks);
                        }
                        nodeContent.Node.Links = newLinks;
                        foreach (Link link in nodeContent.Node.Links)
                        {
                            Node node = new Node(link.Uri.ToString());
                            if (!myBaseline.ContainsKey(node.Key) && !newNodes.ContainsKey(node.Key))
                            {
                                if (myPruner.ShouldPursue(node.Uri))
                                {
                                    newNodes.Add(node.Key, node);
                                }
                            }
                        }
                    }
                }
            }
            myCrawlListener.MessageLogged("Adding " + newNodes.Count.ToString() + " node(s) from crawl evaluation of links");
            foreach (var node in newNodes)
            {
                NodeContent newNodeContent = new NodeContent(node.Value, string.Empty);
                myBaseline.Add(newNodeContent.Node.Key, newNodeContent);
                myCrawlListener.NodeRegistered(beatId, newNodeContent, ((int)HttpStatusCode.NoContent).ToString());
            }

            myCrawlListener.CrawlBeatEnd(beatId);
            return true;
        }

        private string ReadUri(Uri uri, out HttpStatusCode status)
        {
            Stream data = null;
            StreamReader reader = null;
            string s = string.Empty;
            status = HttpStatusCode.NoContent;

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(uri);
            request.Headers.Add("user-agent", userAgent);

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                status = response.StatusCode;
                if (status == HttpStatusCode.OK)
                {
                    data = response.GetResponseStream();
                    try
                    {
                        reader = new StreamReader(data);
                        s = reader.ReadToEnd();
                    }
                    finally
                    {
                        if (reader != null)
                            reader.Close();
                    }
                }
            }
            catch (WebException ex)
            {
                var errorResponse = ex.Response as HttpWebResponse;
                if (errorResponse != null)
                    status = errorResponse.StatusCode;
                myCrawlListener.ExceptionRaised(this, ex);
            }
            catch (Exception ex)
            {
                myCrawlListener.ExceptionRaised(this, ex);
            }
            finally
            {
                if(data != null)
                    data.Close();
            }
            return s;
        }

        public void Stop()
        {
            // TODO: other shutdown & clean up tasks.
            myCrawlListener.CrawlSessionEnd(sessionId);
        }
    }
}
