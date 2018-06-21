using System;
using StackExchange.NetGain;
using StackExchange.NetGain.WebSockets;

namespace WinterIsComing.Server
{
    class GameLoopHandler : IGameLoopHandler
    {
        private readonly IGameCommandFactory gameCommandFactory;
        private readonly IGameCommandDispatcher commandDispatcher;

        public GameLoopHandler(IGameCommandFactory gameCommandFactory, IGameCommandDispatcher commandDispatcher)
        {
            this.gameCommandFactory = gameCommandFactory;
            this.commandDispatcher = commandDispatcher;
        }

        public void GameLoop(Message msg)
        {
            try
            {
                var command = gameCommandFactory.BuildFrom(msg.Value?.ToString());
                var response = commandDispatcher.Dispatch(command);

                msg.Connection.Send(msg.Context, response);

                //var conn = (WebSocketConnection)msg.Connection;
                //var reply = $"reply:{msg.Value} / {conn.Host} / {conn.RequestLine} / {conn.Id}";
                //Console.WriteLine($"[server] {msg.Value}, ");
                //var reply = $"Command: {command.GetType()}";
                //msg.Connection.Send(msg.Context, reply);
            }
            catch (InvalidGameCommandException e)
            {
                Console.WriteLine($"Invalid game command `{msg.Value}`: {e.Message}");
                var reply = $"Invalid game command `{msg.Value}`:  {e.Message}";
                msg.Connection.Send(msg.Context, reply);
            }
            catch (Exception e)
            {
                // swallow exception, and report internal server error
                // do not bring down server

                Console.WriteLine($"Internval server error: {e.Message}, {e}");
                var reply = $"Server error. Report issue";
                msg.Connection.Send(msg.Context, reply);
            }
        }
    }
}