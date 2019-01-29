using System;
using System.Collections.Generic;
using System.Text;
using Hyperippe.GraphModel;

namespace Hyperippe.Workers
{
    public interface ICrawlRecorder
    {
        void NodeCreated(NodeContent nodeContent);
        void ChangeDetected(NodeContent oldNodeContent, string newContent);
        void LinkChangeDetected(NodeContent oldNodeContent, List<Link> newLinks);
        void ExceptionRaised(object caller, Exception ex);
        void LogMessage(string text);
    }
}
