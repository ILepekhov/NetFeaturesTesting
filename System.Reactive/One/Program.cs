using System;
using System.Collections.Generic;

namespace One
{
    class Program
    {
        static void Main()
        {
            FakeStockTimer stockTimer = new();
            // StockMonitor stockMonitor = new(stockTimer);
            StockMonitorWithObservable stockMonitor = new(stockTimer);

            Console.WriteLine("Start polling...");

            stockTimer.StartPolling(new List<string> { "FXUS", "FXCN", "FXIT" });

            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    Console.WriteLine("Stop polling...");

                    stockTimer.StopPolling();
                    stockMonitor.Dispose();

                    break;
                }
                else
                {
                    Console.WriteLine("Press 'Enter' to exit");
                }
            }

        }
    }
}
