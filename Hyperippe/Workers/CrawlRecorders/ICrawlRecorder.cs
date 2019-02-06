using System;
using System.Collections.Generic;
using System.Text;
using Hyperippe.GraphModel;

namespace Hyperippe.Workers
{
    /// <summary>
    /// The ICrawlRecorder interface is called by the Spider as it crawls nodes. Each method is used to
    /// report a specific change, along with some parameters with details about it. This is used for
    /// dependency injection.
    /// </summary>
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
