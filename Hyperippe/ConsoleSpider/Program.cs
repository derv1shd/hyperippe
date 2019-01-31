using System;
using System.Collections.Generic;
using Hyperippe.GraphModel;
using Hyperippe.Workers;

namespace Hyperippe.ConsoleSpider
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            List<Uri> targets = Baseline.ToUriList(args);
            Baseline baseline = new Baseline(targets, 1500);
            Console.WriteLine("Baseline contain(s) " + baseline.Count.ToString() + " node(s)");
            ConsoleCrawlRecorder listener = new ConsoleCrawlRecorder();
            Spider spider = new Spider(baseline, new Pruner(targets, listener, 1000), listener);
            Console.WriteLine("Spider initialized.");

            do
            {
                spider.Crawl();
                Console.WriteLine("--> Baseline contain(s) " + baseline.Count.ToString() + " node(s) --> press a key to exit");
                System.Threading.Thread.Sleep(5000);
            } while (!Console.KeyAvailable);

            Console.WriteLine("End.");
        }
    }
}
