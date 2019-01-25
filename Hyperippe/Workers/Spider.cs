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
        private ICrawlListener myCrawlListener;
        private WebClient webClient;
        private string userAgent { get => "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36";  }

        public Spider(Baseline baseline, ICrawlListener crawlerReport)
        {
            myCrawlListener = crawlerReport ?? throw new ArgumentNullException(nameof(crawlerReport));

            myBaseline = baseline ?? throw new ArgumentNullException(nameof(baseline));
            webClient = new WebClient();
            foreach (var valuePair in myBaseline)
            {
                NodeContent nodeContent = valuePair.Value;
                string current = ReadUri(nodeContent.Node.Uri);
                nodeContent.Update(current);
                myCrawlListener.NodeCreated(nodeContent);
            }
        }

        public bool Crawl()
        {
            foreach(var valuePair in myBaseline)
            {
                NodeContent nodeContent = valuePair.Value;
                string current = ReadUri(nodeContent.Node.Uri);

                if(current!= nodeContent.Content)
                {
                    if (myCrawlListener.ChangeDetected(nodeContent, current))
                        nodeContent.Update(current);
                }
            }

            return true;
        }

        private string ReadUri(Uri uri)
        {
            webClient.Headers.Add("user-agent", userAgent);

            Stream data = webClient.OpenRead(uri);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();

            return s;
        }
    }
}
