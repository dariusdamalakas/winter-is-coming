namespace WinterIsComing.Server
{
    /// <summary>
    /// Start a new game or join existing
    /// </summary>
    public class StartGameCommand : IGameCommand
    {
        /// <summary>
        /// Plyer name that will join game.
        /// </summary>
        /// <remarks>
        /// A player can play only one game at a time
        /// </remarks>
        public string PlayerName { get; set; }

        /// <summary>
        /// Can be empty. If not provided will create new board
        /// </summary>
        public string BoardName { get; set; }

        public string ConnectionId { get; set; }
    }

    public class PlayerShootCommand : IGameCommand
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string ConnectionId { get; set; }
    }
}