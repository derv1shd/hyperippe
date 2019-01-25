using System;
using System.Collections.Generic;
using System.Text;

namespace Hyperippe.Workers
{
    public class NullCrawlListener : ICrawlListener
    {
        bool ICrawlListener.ChangeDetected(NodeContent oldNodeContent, string newContent)
        {
            return true;
        }

        void ICrawlListener.NodeCreated(NodeContent nodeContent)
        {
            return;
        }
    }
}
