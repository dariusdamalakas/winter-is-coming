namespace WinterIsComing.Server
{
    interface IGameBoardFactory
    {
        IGameBoard New(string boardName);
    }
}