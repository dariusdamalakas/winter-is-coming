namespace WinterIsComing.Server
{
    class QuitGameCommandHandler : IGameCommandHandler
    {
        private readonly IBoardManager boardManager;
        private readonly IBroadcastService broadcastService;

        public QuitGameCommandHandler(IBoardManager boardManager, IBroadcastService broadcastService)
        {
            this.boardManager = boardManager;
            this.broadcastService = broadcastService;
        }

        public void Handle(IGameCommand command)
        {
            var board = boardManager.FindByConnectionId(command.ConnectionId);
            if (board == null)
            {
                broadcastService.Broadcast(command.ConnectionId, "No active game");
                return;
            }

            board.RemovePlayer(command.ConnectionId);
            broadcastService.Broadcast(command.ConnectionId, $"You have left game: {board.Name}");
        }

        public bool CanHandle(IGameCommand command)
        {
            return command is QuitCommand;
        }
    }
}