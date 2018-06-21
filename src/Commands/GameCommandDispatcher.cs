using System.Collections.Generic;
using System.Linq;

namespace WinterIsComing.Server
{
    class GameCommandDispatcher : IGameCommandDispatcher
    {
        private readonly IGameCommandHandler[] handlers;

        public GameCommandDispatcher(IEnumerable<IGameCommandHandler> handlers)
        {
            this.handlers = handlers.ToArray();
        }

        public void Dispatch(IGameCommand command)
        {
            var h = handlers.Single(y => y.CanHandle(command));
            h.Handle(command);
        }
    }
}