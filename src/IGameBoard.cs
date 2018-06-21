namespace WinterIsComing.Server
{
    public interface IGameBoard
    {
        string Name { get; set; }
        bool IsAlreadyJoined(IPlayer player);
        bool IsAlreadyJoined(string connectionId);
        void Join(IPlayer player);

        void Start();
        void Stop();
    }
}