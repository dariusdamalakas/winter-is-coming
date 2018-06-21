namespace WinterIsComing.Server
{
    public interface IBroadcastService
    {
        void Broadcast(string connectionId, string message);
        void Broadcast(IGameBoard gameBoard, string message);
        void BroadcastAll(string message);
    }
}