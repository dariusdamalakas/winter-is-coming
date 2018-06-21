using System;
using StackExchange.NetGain;
using StackExchange.NetGain.WebSockets;

namespace WinterIsComing.Server
{
    class GameLoopHandler : IGameLoopHandler
    {
        private readonly IGameCommandFactory gameCommandFactory;

        public GameLoopHandler(IGameCommandFactory gameCommandFactory)
        {
            this.gameCommandFactory = gameCommandFactory;
        }

        public void GameLoop(Message msg)
        {
            var commands = gameCommandFactory.BuildFrom(msg.Value?.ToString());

            var conn = (WebSocketConnection)msg.Connection;
            var reply = $"reply:{msg.Value} / {conn.Host} / {conn.RequestLine} / {conn.Id}";
            Console.WriteLine($"[server] {msg.Value}, ");
            msg.Connection.Send(msg.Context, reply);
        }
    }
}