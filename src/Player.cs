namespace WinterIsComing.Server
{
    public class Player : IPlayer
    {
        public Player(string playerName, string connectionId)
        {
            this.Name = playerName;
            this.ConnectionId = connectionId;
        }

        public string Name { get; set; }
        public string ConnectionId { get; set; }
        public int Score { get; set; }
    }
}