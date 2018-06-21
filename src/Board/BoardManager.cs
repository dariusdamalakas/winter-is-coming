using System.Collections.Generic;
using System.Linq;

namespace WinterIsComing.Server
{
    class BoardManager : IBoardManager
    {
        private readonly List<IGameBoard> boards = new List<IGameBoard>();
        private static int Boards;
        private readonly IGameBoardFactory boardFactory;

        public BoardManager(IGameBoardFactory boardFactory)
        {
            this.boardFactory = boardFactory;
        }

        public IGameBoard Find(string boardName)
        {
            return boards.SingleOrDefault(y => y.Name == boardName);
        }

        public IGameBoard New(string boardName)
        {
            return CreateNewBoard(boardName);
        }

        public IGameBoard FindByConnectionId(string connectionId)
        {
            return boards.FirstOrDefault(y => y.IsAlreadyJoined(connectionId));
        }

        private IGameBoard CreateNewBoard(string boardName)
        {
            if (string.IsNullOrWhiteSpace(boardName))
            {
                Boards++;
                boardName = $"game-board-{Boards}";
            }

            // todo: detect if board with this name already exists
            var board = boardFactory.New(boardName);

            this.boards.Add(board);
            return board;
        }
    }
}