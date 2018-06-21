namespace WinterIsComing.Server
{
    class StartGameCommandHandler : IGameCommandHandler
    {
        private readonly IBoardManager boardManager;

        public StartGameCommandHandler(IBoardManager boardManager)
        {
            this.boardManager = boardManager;
        }

        public string Handle(IGameCommand command)
        {
            var startGameCommand = command as StartGameCommand;

            var alreadyPlayingOnBoard = boardManager.IsAlreadyPlaying(startGameCommand.PlayerName);
            if (alreadyPlayingOnBoard != null)
                return $"Player {startGameCommand.PlayerName} already playing on board {alreadyPlayingOnBoard.Name}";

            var board = boardManager.FindOrNewBoard(startGameCommand.BoardName);

            if (board.IsAlreadyJoined(startGameCommand.PlayerName))
                return $"Player {startGameCommand.PlayerName} already playing on board {board.Name}";

            board.Join(startGameCommand.PlayerName);
            return $"Player {startGameCommand.PlayerName} joined board {board.Name}";
        }

        public bool CanHandle(IGameCommand command)
        {
            return command is StartGameCommand;
        }
    }
}