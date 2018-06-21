namespace WinterIsComing.Server
{
    public interface IPlayer
    {
        string Name { get; set; }
        string ConnectionId { get; set; }
        int Score { get; set; }
    }
}