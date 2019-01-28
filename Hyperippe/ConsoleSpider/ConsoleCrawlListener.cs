using System;
using System.Collections.Generic;
using System.Text;
using Hyperippe.GraphModel;
using Hyperippe.Workers;

namespace Hyperippe.ConsoleSpider
{
    class ConsoleCrawlListener : Hyperippe.Workers.ICrawlListener
    {
        void ICrawlListener.ChangeDetected(NodeContent oldNodeContent, string newContent)
        {
            Console.WriteLine("Change detected on " + oldNodeContent.Node.Name + ", old length: " + oldNodeContent.Content.Length.ToString() + ", new length: " + newContent.Length.ToString());
        }

        void ICrawlListener.ExceptionRaised(object caller, Exception ex)
        {
            Console.WriteLine("Exception thrown on " + caller.ToString() + ", exception: " + ex.Message);
        }

        void ICrawlListener.LinkChangeDetected(NodeContent oldNodeContent, List<Link> newLinks)
        {
            Console.WriteLine("Link change detected on " + oldNodeContent.Node.Name + ", previous link number: " + oldNodeContent.Node.Links.Count.ToString() + ", link number: " + newLinks.Count.ToString());
        }

        void ICrawlListener.LogMessage(string text)
        {
            Console.WriteLine(text);
        }

        void ICrawlListener.NodeCreated(NodeContent nodeContent)
        {
            Console.WriteLine("Created node for " + nodeContent.Node.Name + " URL: " + nodeContent.Node.Uri.ToString() + ", link(s): " + nodeContent.Node.Links.Count.ToString());
        }
    }
}
