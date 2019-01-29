using System;
using System.Collections.Generic;
using System.Text;
using Hyperippe.GraphModel;
using Hyperippe.Workers;

namespace Hyperippe.ConsoleSpider
{
    class ConsoleCrawlRecorder : Hyperippe.Workers.ICrawlRecorder
    {
        void ICrawlRecorder.ChangeDetected(NodeContent oldNodeContent, string newContent)
        {
            Console.WriteLine("Change detected on " + oldNodeContent.Node.Name + ", old length: " + oldNodeContent.Content.Length.ToString() + ", new length: " + newContent.Length.ToString());
        }

        void ICrawlRecorder.ExceptionRaised(object caller, Exception ex)
        {
            Console.WriteLine("Exception thrown on " + caller.ToString() + ", exception: " + ex.Message);
        }

        void ICrawlRecorder.LinkChangeDetected(NodeContent oldNodeContent, List<Link> newLinks)
        {
            Console.WriteLine("Link change detected on " + oldNodeContent.Node.Name + ", previous link number: " + oldNodeContent.Node.Links.Count.ToString() + ", link number: " + newLinks.Count.ToString());
        }

        void ICrawlRecorder.LogMessage(string text)
        {
            Console.WriteLine(text);
        }

        void ICrawlRecorder.NodeCreated(NodeContent nodeContent)
        {
            Console.WriteLine("Created node for " + nodeContent.Node.Name + " URL: " + nodeContent.Node.Uri.ToString() + ", link(s): " + nodeContent.Node.Links.Count.ToString());
        }
    }
}
