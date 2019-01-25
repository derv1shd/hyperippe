using System;
using System.Collections.Generic;
using System.Text;
using Hyperippe.Workers;

namespace Hyperippe.ConsoleSpider
{
    class ConsoleCrawlListener : Hyperippe.Workers.ICrawlListener
    {
        bool ICrawlListener.ChangeDetected(NodeContent oldNodeContent, string newContent)
        {
            Console.WriteLine("Change detected on " + oldNodeContent.Node.Name + ", old length: " + oldNodeContent.Content.Length.ToString() + ", new length: " + newContent.Length.ToString());
            return true;
        }

        void ICrawlListener.NodeCreated(NodeContent nodeContent)
        {
            Console.WriteLine("Created node for " + nodeContent.Node.Name + " URL: " + nodeContent.Node.Uri.ToString());
        }
    }
}
