using System;
using System.Collections.Generic;
using System.Text;
using Hyperippe.GraphModel;

namespace Hyperippe.Workers
{
    public interface ICrawlRecorder
    {
        long CrawlSessionBegin();
        int CrawlBeatBegin(long sessionId);
        void NodeRegistered(int beatId, NodeContent nodeContent);
        void NodeChangeDetected(int beatId, NodeContent oldNodeContent, string newContent);
        void NodeLinkChangeDetected(int beatId, NodeContent oldNodeContent, List<Link> newLinks);
        void ExceptionRaised(object caller, Exception ex);
        void MessageLogged(string text);
        void CrawlBeatEnd(int beatId);
        void CrawlSessionEnd(long sessionId);
    }
}
