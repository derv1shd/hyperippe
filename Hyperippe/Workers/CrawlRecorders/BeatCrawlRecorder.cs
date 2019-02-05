using System;
using System.Collections.Generic;
using System.Text;
using Hyperippe.GraphModel;

namespace Hyperippe.Workers
{
    public class BeatCrawlRecorder : Hyperippe.Workers.ICrawlRecorder
    {
        protected long sessionId;
        protected int beatId = 0;
        private Beatline myBeatline;

        public BeatCrawlRecorder(Beatline beatline)
        {
            myBeatline = beatline;
        }

        long ICrawlRecorder.CrawlSessionBegin()
        {
            sessionId = DateTime.Now.ToBinary();
            return sessionId;
        }

        void ICrawlRecorder.CrawlSessionBegin(long session)
        {
            sessionId = session;
        }

        int ICrawlRecorder.CrawlBeatBegin(long session)
        {
            beatId++;
            myBeatline.Insert(beatId, new Beat(sessionId, beatId));
            return beatId;
        }

        void ICrawlRecorder.CrawlBeatBegin(int beat)
        {
            //while(myBeatline.Count - 1 < beat)
            //{
            //    myBeatline.Add(new Beat(sessionId, myBeatline.Count - 1));
            //}
            myBeatline.Add(new Beat(sessionId, beat));
        }

        void ICrawlRecorder.NodeRegistered(int beatId, NodeContent nodeContent, string status)
        {
            NodeState state = new NodeState(nodeContent.Node, status, false, false, nodeContent.ContentType, nodeContent.ContentLength);
            myBeatline[beatId].Add(state);
        }

        void ICrawlRecorder.NodeStatusReported(int beatId, NodeContent nodeContent, string status)
        {
            if (!myBeatline[beatId].Contains(nodeContent.Node))
                myBeatline[beatId].Add(new NodeState(nodeContent.Node, status, false, false, nodeContent.ContentType, nodeContent.ContentLength));
            else
                myBeatline[beatId][myBeatline[beatId].IndexOf(nodeContent.Node)].Update(status);
        }

        void ICrawlRecorder.NodeChangeDetected(int beatId, NodeContent oldNodeContent, string newContent, string newContentType, long newContentLength, string status)
        {
            if (!myBeatline[beatId].Contains(oldNodeContent.Node))
                myBeatline[beatId].Add(new NodeState(oldNodeContent.Node, status, true, false, newContentType, newContentLength));
            else
                myBeatline[beatId][myBeatline[beatId].IndexOf(oldNodeContent.Node)].Update(status, true, false, newContentType, newContentLength);
        }

        void ICrawlRecorder.NodeLinkChangeDetected(int beatId, NodeContent oldNodeContent, List<Link> newLinks)
        {
            myBeatline[beatId][myBeatline[beatId].IndexOf(oldNodeContent.Node)].Update(true, true);
        }

        void ICrawlRecorder.ExceptionRaised(object caller, Exception ex)
        {
        }

        void ICrawlRecorder.MessageLogged(string text)
        {
        }

        void ICrawlRecorder.CrawlBeatEnd(int beatId)
        {
        }

        void ICrawlRecorder.CrawlSessionEnd(long sessionId)
        {
        }
    }
}
