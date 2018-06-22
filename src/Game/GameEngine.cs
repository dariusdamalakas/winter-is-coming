using System;

namespace WinterIsComing.Server
{
    class GameEngine : IGameEngine
    {
        private readonly IGameLoopHandler gameLoopHandler;
        private readonly IGameNetwork gameNetwork;

        public GameEngine(IGameLoopHandler gameLoopHandler, IGameNetwork gameNetwork)
        {
            this.gameLoopHandler = gameLoopHandler;
            this.gameNetwork = gameNetwork;
        }

        public void StartGameLoop()
        {
            Console.WriteLine($"Starting up");
            gameNetwork.Start();
            gameNetwork.SetOnMessageReceived(gameLoopHandler.GameLoop);
            Console.WriteLine("Server running");

            Console.ReadKey();
            
            gameNetwork.BroadcastAll("Server quitting. Bye bye");
            gameNetwork.Stop();
        }
    }
}