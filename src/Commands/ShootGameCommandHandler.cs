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

            board.RemoveGameObject(hitObject);
            if (board.GameObjects.Count < 1)
            {
                broadcastService.Broadcast(board, $"Oh no, they are respawning. Now there will be 2 zombies!");
                boardActions.ScheduleNewZombie(board);
                boardActions.ScheduleNewZombie(board, 1000.Random(5000));
            }

            player.Score += 1;

            broadcastService.Broadcast(board, $"BOOM {player.Name} {player.Score} {hitObject.Name}");
        }

        
        public bool CanHandle(IGameCommand command)
        {
            return command is ShootCommand;
        }
    }
}