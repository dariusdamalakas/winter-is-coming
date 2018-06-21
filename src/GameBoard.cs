using System.Collections.Generic;

namespace WinterIsComing.Server
{
    class GameBoard : IGameBoard
    {
        // TODO - thread safety?
        private readonly List<string> players = new List<string>();
        public string Name { get; set; }

        public GameBoard(string boardName)
        {
            this.Name = boardName;

        }

        public bool IsAlreadyJoined(string playerName)
        {
            return players.Contains(playerName);
        }

        public void Join(string playerName)
        {
            this.players.Add(playerName);
        }
    }
}