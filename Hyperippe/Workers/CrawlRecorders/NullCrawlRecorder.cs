using System;
using System.Collections.Generic;
using System.Text;
using Hyperippe.GraphModel;

namespace Hyperippe.Workers
{
    public class NullCrawlRecorder : ICrawlRecorder
    {
        private long sessionId;
        private int beatId = 0;

        long ICrawlRecorder.CrawlSessionBegin()
        {
            sessionId = DateTime.Now.ToBinary();
            return sessionId;
        }

        int ICrawlRecorder.CrawlBeatBegin(long sessionId)
        {
            return ++beatId;
        }

        void ICrawlRecorder.NodeChangeDetected(int beatId, NodeContent oldNodeContent, string newContent)
        {
            return;
        }

        void ICrawlRecorder.ExceptionRaised(object caller, Exception ex)
        {
            return;
        }

        void ICrawlRecorder.NodeLinkChangeDetected(int beatId, NodeContent oldNodeContent, List<Link> newLinks)
        {
            return;
        }

        void ICrawlRecorder.MessageLogged(string text)
        {
            return;
        }

        void ICrawlRecorder.NodeRegistered(int beatId, NodeContent nodeContent)
        {
            return;
        }

        void ICrawlRecorder.CrawlBeatEnd(int beatId)
        {
            return;
        }

        void ICrawlRecorder.CrawlSessionEnd(long sessionId)
        {
            return;
        }
    }
}
