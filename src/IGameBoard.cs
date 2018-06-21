namespace WinterIsComing.Server
{
    public interface IGameBoard
    {
        string Name { get; set; }
        bool IsAlreadyJoined(string playerName);
        void Join(string playerName);
    }
}