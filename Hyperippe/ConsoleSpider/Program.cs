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
            Baseline baseline = new Baseline(new List<string>(args));
            Console.WriteLine("Baseline contain(s) " + baseline.Count.ToString() + " node(s)");
            ConsoleCrawlListener listener = new ConsoleCrawlListener();
            Spider spider = new Spider(baseline, new Pruner(listener), listener);
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
