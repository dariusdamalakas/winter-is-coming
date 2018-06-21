using System;
using System.Net;
using StackExchange.NetGain;
using StackExchange.NetGain.WebSockets;

namespace WinterIsComing.Server
{
    class GameEngine : IGameEngine
    {
        private readonly IGameLoopHandler gameLoopHandler;

        public GameEngine(IGameLoopHandler gameLoopHandler)
        {
            this.gameLoopHandler = gameLoopHandler;
        }

        public void StartGameLoop()
        {
            var endpoint = new IPEndPoint(IPAddress.Loopback, 6002);
            using (var server = new TcpServer())
            {
                server.ProtocolFactory = WebSocketsSelectorProcessor.Default;
                server.ConnectionTimeoutSeconds = 60;

                server.Received += gameLoopHandler.GameLoop;

                /*server.Received += msg =>
                {
                    var conn = (WebSocketConnection)msg.Connection;
                    var reply = $"reply:{msg.Value} / {conn.Host} / {conn.RequestLine} / {conn.Id}";
                    Console.WriteLine($"[server] {msg.Value}, ");
                    msg.Connection.Send(msg.Context, reply);
                };
                */
                server.Start("abc", endpoint);
                Console.WriteLine("Server running");

                Console.ReadKey();
            }
        }
    }
}