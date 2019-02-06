using System;
using System.Collections.Generic;
using System.Text;
using Hyperippe.GraphModel;

namespace Hyperippe.Workers
{
    /// <summary>
    /// The MultiplexCrawlRecorder is a standard ICrawlRecorder implementation to allow for several recorders
    /// to be used. Its constructor has a list of recorders, and this class replicates the calls that it
    /// receives to those recorders. A typical usage scenario would be to instance a BeatCrawlRecorder, and other
    /// implementations of recorders that i.e. print changes to the screen of reports them in some visualization,
    /// and all these are passed to a MultiplexCrawlRecorder to make sure all changes are received by all
    /// ICrawlRecorder implementations.
    /// </summary>
    public class MultiplexCrawlRecorder : ICrawlRecorder
    {
        protected long sessionId;
        protected int beatId = -1;
        protected List<ICrawlRecorder> recorders;

        public MultiplexCrawlRecorder(List<ICrawlRecorder> crawlRecorders)
        {
            recorders = crawlRecorders;
        }

        public MultiplexCrawlRecorder(ICrawlRecorder[] crawlRecorders)
        {
            recorders = new List<ICrawlRecorder>();
            foreach(var rec in crawlRecorders)
            {
                recorders.Add(rec);
            }
        }

        long ICrawlRecorder.CrawlSessionBegin()
        {
            sessionId = DateTime.Now.ToBinary();
            foreach(var recorder in recorders)
            {
                recorder.CrawlSessionBegin(sessionId);
            }
            return sessionId;
        }

        void ICrawlRecorder.CrawlSessionBegin(long session)
        {
            sessionId = session;
            foreach (var recorder in recorders)
            {
                recorder.CrawlSessionBegin(sessionId);
            }
        }

        int ICrawlRecorder.CrawlBeatBegin(long sessionId)
        {
            beatId++;
            foreach (var recorder in recorders)
            {
                recorder.CrawlBeatBegin(beatId);
            }
            return beatId;
        }

        void ICrawlRecorder.CrawlBeatBegin(int beat)
        {
            beatId = beat;
            foreach (var recorder in recorders)
            {
                recorder.CrawlBeatBegin(beatId);
            }
        }

        void ICrawlRecorder.NodeRegistered(int beatId, NodeContent nodeContent, string status)
        {
            foreach (var recorder in recorders)
            {
                recorder.NodeRegistered(beatId, nodeContent, status);
            }
        }

        void ICrawlRecorder.NodeStatusReported(int beatId, NodeContent nodeContent, string status)
        {
            foreach (var recorder in recorders)
            {
                recorder.NodeStatusReported(beatId, nodeContent, status);
            }
        }

        void ICrawlRecorder.NodeChangeDetected(int beatId, NodeContent oldNodeContent, string newContent, string newContentType, long newContentLength, string status)
        {
            foreach (var recorder in recorders)
            {
                recorder.NodeChangeDetected(beatId, oldNodeContent, newContent, newContentType, newContentLength, status);
            }
        }

        void ICrawlRecorder.NodeLinkChangeDetected(int beatId, NodeContent oldNodeContent, List<Link> newLinks)
        {
            foreach (var recorder in recorders)
            {
                recorder.NodeLinkChangeDetected(beatId, oldNodeContent, newLinks);
            }
        }

        void ICrawlRecorder.ExceptionRaised(object caller, Exception ex)
        {
            foreach (var recorder in recorders)
            {
                recorder.ExceptionRaised(caller, ex);
            }
        }

        void ICrawlRecorder.MessageLogged(string text)
        {
            foreach (var recorder in recorders)
            {
                recorder.MessageLogged(text);
            }
        }

        void ICrawlRecorder.CrawlBeatEnd(int beatId)
        {
            foreach (var recorder in recorders)
            {
                recorder.CrawlBeatEnd(beatId);
            }
        }

        void ICrawlRecorder.CrawlSessionEnd(long sessionId)
        {
            foreach (var recorder in recorders)
            {
                recorder.CrawlSessionEnd(sessionId);
            }
        }
    }
}
