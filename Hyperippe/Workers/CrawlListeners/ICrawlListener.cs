using System;
using System.Collections.Generic;
using System.Text;

namespace Hyperippe.Workers
{
    public interface ICrawlListener
    {
        void NodeCreated(NodeContent nodeContent);
        bool ChangeDetected(NodeContent oldNodeContent, string newContent);
    }
}
