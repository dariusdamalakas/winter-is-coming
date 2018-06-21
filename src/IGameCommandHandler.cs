namespace WinterIsComing.Server
{
    interface IGameCommandHandler
    {
        void Handle(IGameCommand command);
        bool CanHandle(IGameCommand command);
    }
}