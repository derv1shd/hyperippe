using System;
using System.Collections.Generic;
using System.Text;
using Hyperippe.GraphModel;

namespace Hyperippe.Workers
{
    public class NullCrawlRecorder : ICrawlRecorder
    {
        void ICrawlRecorder.ChangeDetected(NodeContent oldNodeContent, string newContent)
        {
            return;
        }

        void ICrawlRecorder.ExceptionRaised(object caller, Exception ex)
        {
            return;
        }

        void ICrawlRecorder.LinkChangeDetected(NodeContent oldNodeContent, List<Link> newLinks)
        {
            return;
        }

        void ICrawlRecorder.LogMessage(string text)
        {
            return;
        }

        void ICrawlRecorder.NodeCreated(NodeContent nodeContent)
        {
            return;
        }
    }
}
