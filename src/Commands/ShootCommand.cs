namespace WinterIsComing.Server
{
    public class ShootCommand : IGameCommand
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string ConnectionId { get; set; }
    }
}