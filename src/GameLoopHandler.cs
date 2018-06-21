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
                command.ConnectionId = msg.Connection.Id.ToString();
                commandDispatcher.Dispatch(command);

                //msg.Connection.Send(msg.Context, response);
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