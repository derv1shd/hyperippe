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
            Beatline beatline = new Beatline();
            Console.WriteLine("Baseline contain(s) " + baseline.Count.ToString() + " node(s)");
            ConsoleCrawlRecorder consoleListener = new ConsoleCrawlRecorder();
            BeatCrawlRecorder recorder = new BeatCrawlRecorder(beatline);
            MultiplexCrawlRecorder listener = new MultiplexCrawlRecorder(new ICrawlRecorder[] { consoleListener, recorder});
            Spider spider = new Spider(baseline, new Pruner(targets, listener, 10), listener);
            Console.WriteLine("Spider initialized.");

            do
            {
                spider.Crawl();
                Console.WriteLine("--> Baseline contain(s) " + baseline.Count.ToString() + " node(s)");
                Console.WriteLine("--> Beatline contain(s) " + beatline.Count.ToString() + " beat(s)");
                if(beatline.Count > 0)
                    Console.WriteLine("--> Last beat contain(s) " + beatline[beatline.Count - 1].Count.ToString() + " node(s)");
                Console.WriteLine("--> press a key to exit");
                System.Threading.Thread.Sleep(5000);
            } while (!Console.KeyAvailable);

            Console.WriteLine("End.");
        }
    }
}
