namespace WinterIsComing.Server
{
    public interface IBoardManager
    {
        IGameBoard FindOrNewBoard(string boardName);
        IGameBoard IsAlreadyPlaying(string playerName);
    }
}