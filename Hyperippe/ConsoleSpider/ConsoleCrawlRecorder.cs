using System;
using System.Collections.Generic;
using System.Text;
using Hyperippe.GraphModel;
using Hyperippe.Workers;

namespace Hyperippe.ConsoleSpider
{
    public class ConsoleCrawlRecorder : Hyperippe.Workers.ICrawlRecorder
    {
        private long sessionId;
        private int beatId = 0;

        long ICrawlRecorder.CrawlSessionBegin()
        {
            Console.WriteLine("Session begin");
            sessionId = DateTime.Now.ToBinary();
            return sessionId;
        }

        int ICrawlRecorder.CrawlBeatBegin(long sessionId)
        {
            beatId++;
            Console.WriteLine("Beat " + beatId.ToString() + " began");
            return beatId;
        }

        void ICrawlRecorder.NodeChangeDetected(int beatId, NodeContent oldNodeContent, string newContent)
        {
            Console.WriteLine("Change detected on " + oldNodeContent.Node.Name + ", old length: " + oldNodeContent.Content.Length.ToString() + ", new length: " + newContent.Length.ToString());
        }

        void ICrawlRecorder.ExceptionRaised(object caller, Exception ex)
        {
            Console.WriteLine("Exception thrown on " + caller.ToString() + ", exception: " + ex.Message);
        }

        void ICrawlRecorder.NodeLinkChangeDetected(int beatId, NodeContent oldNodeContent, List<Link> newLinks)
        {
            Console.WriteLine("Link change detected on " + oldNodeContent.Node.Name + ", previous link number: " + oldNodeContent.Node.Links.Count.ToString() + ", link number: " + newLinks.Count.ToString());
        }

        void ICrawlRecorder.MessageLogged(string text)
        {
            Console.WriteLine(text);
        }

        void ICrawlRecorder.NodeRegistered(int beatId, NodeContent nodeContent)
        {
            Console.WriteLine("Created node for " + nodeContent.Node.Name + " URL: " + nodeContent.Node.Uri.ToString() + ", link(s): " + nodeContent.Node.Links.Count.ToString());
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
