namespace WinterIsComing.Server
{
    interface IGameCommandHandler
    {
        string Handle(IGameCommand command);
        bool CanHandle(IGameCommand command);
    }
}