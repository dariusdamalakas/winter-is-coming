using Autofac;

namespace WinterIsComing.Server
{
    class NightWatchGame
    {
        public static IGameEngine Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<GameLoopHandler>()
                .As<IGameLoopHandler>()
                .SingleInstance();

            builder.RegisterType<GameEngine>()
                .As<IGameEngine>()
                .SingleInstance();

            builder.RegisterType<GameCommandFactory>()
                .As<IGameCommandFactory>()
                .SingleInstance();

            builder.RegisterType<GameCommandDispatcher>()
                .As<IGameCommandDispatcher>()
                .SingleInstance();

            builder.RegisterType<StartGameCommandHandler>()
                .As<IGameCommandHandler>()
                .SingleInstance();

            builder.RegisterType<BoardManager>()
                .As<IBoardManager>()
                .SingleInstance();

        var container = builder.Build();

            return container.Resolve<IGameEngine>();
        }
    }
}