namespace WinterIsComing.Server
{
    class ShootGameCommandHandler : IGameCommandHandler
    {
        private readonly IBoardManager boardManager;
        private readonly IBroadcastService broadcastService;
        private readonly IBoardActions boardActions;

        public ShootGameCommandHandler(IBoardManager boardManager, IBroadcastService broadcastService, IBoardActions boardActions)
        {
            this.boardManager = boardManager;
            this.broadcastService = broadcastService;
            this.boardActions = boardActions;
        }

        public void Handle(IGameCommand command)
        {
            var board = boardManager.FindByConnectionId(command.ConnectionId);
            if (board == null)
            {
                broadcastService.Broadcast(command.ConnectionId, "No active games!");
                return;
            }

            var shootCommand = command as ShootCommand;

            var player = board.FindPlayer(command.ConnectionId);
            var hitObject = board.ObjectAt(shootCommand.X, shootCommand.Y);
            if (hitObject == null)
            {
                broadcastService.Broadcast(command.ConnectionId, $"BOOM {player.Name} {player.Score}");
                return;
            }

            boardActions.ScheduleNewZombie(board);
            player.Score += 1;
            board.RemoveGameObject(hitObject);

            broadcastService.Broadcast(board, $"BOOM {player.Name} {player.Score} {hitObject.Name}");
        }

        
        public bool CanHandle(IGameCommand command)
        {
            return command is ShootCommand;
        }
    }
}