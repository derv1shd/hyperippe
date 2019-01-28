using System;
using System.Collections.Generic;
using System.Text;
using Hyperippe.GraphModel;

namespace Hyperippe.Workers
{
    public class NullCrawlListener : ICrawlListener
    {
        void ICrawlListener.ChangeDetected(NodeContent oldNodeContent, string newContent)
        {
            return;
        }

        void ICrawlListener.ExceptionRaised(object caller, Exception ex)
        {
            return;
        }

        void ICrawlListener.LinkChangeDetected(NodeContent oldNodeContent, List<Link> newLinks)
        {
            return;
        }

        void ICrawlListener.LogMessage(string text)
        {
            return;
        }

        void ICrawlListener.NodeCreated(NodeContent nodeContent)
        {
            return;
        }
    }
}
