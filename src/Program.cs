
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;


namespace WinterIsComing.Server
{
    class Program
    {
        private static readonly AutoResetEvent waitHandle = new AutoResetEvent(false);

        static void Main()
        {
            Task.Run(() => RunGame());

            // Handle Control+C or Control+Break
            Console.CancelKeyPress += (o, e) =>
            {
                Console.WriteLine("Bye bye.");

                // Allow the manin thread to continue and exit...
                waitHandle.Set();
            };

            // Wait
            waitHandle.WaitOne();
        }

        private static void RunGame()
        {
            var engine = NightWatchGame.Build();
            engine.StartGameLoop();

            // keep game running
            while (true)
            {
            }
        }
    }
}
