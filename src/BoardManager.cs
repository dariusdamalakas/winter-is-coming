using System.Collections.Generic;
using System.Linq;

namespace WinterIsComing.Server
{
    class BoardManager : IBoardManager
    {
        private readonly List<IGameBoard> boards = new List<IGameBoard>();
        private static int Boards;

        public IGameBoard FindOrNewBoard(string boardName)
        {
            var board = boards.SingleOrDefault(y => y.Name == boardName);
            if (board != null)
                return board;
            return CreateNewBoard(boardName);
        }

        public IGameBoard IsAlreadyPlaying(string playerName)
        {
            return boards.FirstOrDefault(y => y.IsAlreadyJoined(playerName));
        }

        private IGameBoard CreateNewBoard(string boardName)
        {
            if (string.IsNullOrWhiteSpace(boardName))
            {
                Boards++;
                boardName = $"game-board-{Boards}";
            }

            // todo: detect if board with this name already exists
            var board = new GameBoard(boardName);
            this.boards.Add(board);
            return board;
        }
    }
}