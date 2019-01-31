using System;
using System.Collections.Generic;
using System.Text;
using Hyperippe.GraphModel;
using Hyperippe.Workers;

namespace Hyperippe.ConsoleSpider
{
    public class ConsoleCrawlRecorder : Hyperippe.Workers.ICrawlRecorder
    {
        protected long sessionId;
        protected int beatId = 0;

        long ICrawlRecorder.CrawlSessionBegin()
        {
            Console.WriteLine("Session begin");
            sessionId = DateTime.Now.ToBinary();
            return sessionId;
        }

        void ICrawlRecorder.CrawlSessionBegin(long session)
        {
            sessionId = session;
            Console.WriteLine("Session begin");
        }

        int ICrawlRecorder.CrawlBeatBegin(long sessionId)
        {
            beatId++;
            Console.WriteLine("Beat " + beatId.ToString() + " began");
            return beatId;
        }

        void ICrawlRecorder.CrawlBeatBegin(int beat)
        {
            beatId = beat;
            Console.WriteLine("Beat " + beatId.ToString() + " began");
        }

        void ICrawlRecorder.NodeRegistered(int beatId, NodeContent nodeContent, string status)
        {
            Console.WriteLine("Created node for " + nodeContent.Node.Name + " URL: " + nodeContent.Node.Uri.ToString() + " (" + status + "), link(s): " + nodeContent.Node.Links.Count.ToString());
        }

        void ICrawlRecorder.NodeStatusReported(int beatId, NodeContent nodeContent, string status)
        {
            Console.WriteLine("Status " + status + " for node " + nodeContent.Node.Name + " URL: " + nodeContent.Node.Uri.ToString());
        }

        void ICrawlRecorder.NodeChangeDetected(int beatId, NodeContent oldNodeContent, string newContent, string status)
        {
            Console.WriteLine("Change detected on " + oldNodeContent.Node.Name + " (" + status + "), old length: " + oldNodeContent.Content.Length.ToString() + ", new length: " + newContent.Length.ToString());
        }

        void ICrawlRecorder.NodeLinkChangeDetected(int beatId, NodeContent oldNodeContent, List<Link> newLinks)
        {
            Console.WriteLine("Link change detected on " + oldNodeContent.Node.Name + ", previous link number: " + oldNodeContent.Node.Links.Count.ToString() + ", link number: " + newLinks.Count.ToString());
        }

        void ICrawlRecorder.ExceptionRaised(object caller, Exception ex)
        {
            Console.WriteLine("Exception thrown on " + caller.ToString() + ", exception: " + ex.Message);
        }

        void ICrawlRecorder.MessageLogged(string text)
        {
            Console.WriteLine(text);
        }

        void ICrawlRecorder.CrawlBeatEnd(int beatId)
        {
            Console.WriteLine("Beat " + beatId.ToString() + " ended");
            return;
        }

        void ICrawlRecorder.CrawlSessionEnd(long sessionId)
        {
            Console.WriteLine("Session end");
            return;
        }
    }
}
