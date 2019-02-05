using System;
using System.Collections.Generic;
using System.Text;
using Hyperippe.GraphModel;

namespace Hyperippe.Workers
{
    public interface ICrawlRecorder
    {
        long CrawlSessionBegin();
        void CrawlSessionBegin(long sessionId);
        int CrawlBeatBegin(long sessionId);
        void CrawlBeatBegin(int beatIdd);
        void NodeRegistered(int beatId, NodeContent nodeContent, string status);
        void NodeStatusReported(int beatId, NodeContent nodeContent, string status);
        void NodeChangeDetected(int beatId, NodeContent oldNodeContent, string newContent, string newContentType, long newContentLength, string status);
        void NodeLinkChangeDetected(int beatId, NodeContent oldNodeContent, List<Link> newLinks);
        void ExceptionRaised(object caller, Exception ex);
        void MessageLogged(string text);
        void CrawlBeatEnd(int beatId);
        void CrawlSessionEnd(long sessionId);
    }
}
