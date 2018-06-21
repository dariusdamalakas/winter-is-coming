namespace WinterIsComing.Server
{
    public interface IBoardManager
    {
        IGameBoard New(string boardName);
        IGameBoard Find(string boardName);
        IGameBoard FindByConnectionId(string connectionId);
    }
}