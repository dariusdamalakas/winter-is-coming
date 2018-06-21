namespace WinterIsComing.Server
{
    class PlayerStatsCommandHandler : IGameCommandHandler
    {
        private readonly IBoardManager boardManager;
        private readonly IBroadcastService broadcastService;

        public PlayerStatsCommandHandler(IBoardManager boardManager, IBroadcastService broadcastService)
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
            broadcastService.Broadcast(command.ConnectionId, "All player stats:");
            foreach (var p in board.Players)
            {
                var isMe = p.ConnectionId == command.ConnectionId ? " << you" : "";
                broadcastService.Broadcast(command.ConnectionId, $"Player ({p.ConnectionId}): {p.Name}, Score: {p.Score}{isMe}");
            }
        }

        public bool CanHandle(IGameCommand command)
        {
            return command is PlayerStatsCommand;
        }
    }
}