namespace WinterIsComing.Server
{
    public interface IBroadcastService
    {
        void Broadcast(IGameBoard gameBoard, string message);
        void BroadcastAll(string message);
    }
}