
using System;

namespace WinterIsComing.Server
{
    class Program
    {
        static void Main()
        {
            var engine = NightWatchGame.Build();

            engine.StartGameLoop();

            Console.WriteLine("Bye bye; ");
        }
    }
}
