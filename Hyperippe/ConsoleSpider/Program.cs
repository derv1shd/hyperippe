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
            Console.WriteLine("Baseline contain(s) " + baseline.Count.ToString() + " url(s)");
            Spider spider = new Spider(baseline, new ConsoleCrawlListener());
            Console.WriteLine("Spider initialized.");
            Console.WriteLine("(press a key to exit)");

            do
            {
                Console.WriteLine("Checking...");
                spider.Crawl();
                System.Threading.Thread.Sleep(5000);
            } while (!Console.KeyAvailable);

            Console.WriteLine("End.");
        }
    }
}
