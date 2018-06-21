namespace WinterIsComing.Server
{
    class StartGameCommandHandler : IGameCommandHandler
    {
        private readonly IBoardManager boardManager;
        private readonly IBroadcastService broadcastService;

        public StartGameCommandHandler(IBoardManager boardManager, IBroadcastService broadcastService)
        {
            this.boardManager = boardManager;
            this.broadcastService = broadcastService;
        }

        public void Handle(IGameCommand command)
        {
            var startGameCommand = command as StartGameCommand;

            var alreadyPlayingOnBoard = boardManager.FindByConnectionId(startGameCommand.ConnectionId);
            if (alreadyPlayingOnBoard != null)
            {
                broadcastService.Broadcast(command.ConnectionId, $"Player {startGameCommand.PlayerName} already playing on board {alreadyPlayingOnBoard.Name}");
                return;
            }

            var board = boardManager.Find(startGameCommand.BoardName);
            if (board == null)
            {
                board = StartNewBoard(startGameCommand.BoardName);
            }

            if (board.IsAlreadyJoined(startGameCommand.ConnectionId))
            {
                broadcastService.Broadcast(command.ConnectionId, $"Player {startGameCommand.PlayerName} already playing on board {board.Name}");
                return;
            }

            board.Join(new Player(startGameCommand.PlayerName, startGameCommand.ConnectionId));
            broadcastService.Broadcast(command.ConnectionId, $"Player {startGameCommand.PlayerName} joined board {board.Name}");
        }

        private IGameBoard StartNewBoard(string boardName)
        {
            var board = boardManager.New(boardName);

            var zombie = new Zombie("night-king", 0, 0);
            board.AddGameObject(zombie);
            board.Start();

            return board;
        }

        public bool CanHandle(IGameCommand command)
        {
            return command is StartGameCommand;
        }
    }
}