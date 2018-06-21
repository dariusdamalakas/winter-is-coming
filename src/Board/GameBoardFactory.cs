namespace WinterIsComing.Server
{
    class GameBoardFactory : IGameBoardFactory
    {
        private readonly IBroadcastService broadcastService;
        private readonly IBoardActions boardActions;

        public GameBoardFactory(IBroadcastService broadcastService, IBoardActions boardActions)
        {
            this.broadcastService = broadcastService;
            this.boardActions = boardActions;
        }

        public IGameBoard New(string boardName)
        {
            return new GameBoard(boardName, broadcastService, boardActions);
        }
    }
}