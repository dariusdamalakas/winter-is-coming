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

            builder.RegisterType<ShootGameCommandHandler>()
                .As<IGameCommandHandler>()
                .SingleInstance();

            builder.RegisterType<PlayerStatsCommandHandler>()
                .As<IGameCommandHandler>()
                .SingleInstance();

            builder.RegisterType<QuitGameCommandHandler>()
                .As<IGameCommandHandler>()
                .SingleInstance();

            builder.RegisterType<HelpCommandHandler>()
                .As<IGameCommandHandler>()
                .SingleInstance();

            builder.RegisterType<ChatCommandHandler>()
                .As<IGameCommandHandler>()
                .SingleInstance();

            builder.RegisterType<BoardManager>()
                .As<IBoardManager>()
                .SingleInstance();

            builder.RegisterType<GameNework>()
                .As<IBroadcastService>()
                .As<IGameNetwork>()
                .SingleInstance();

            builder.RegisterType<GameBoardFactory>()
                .As<IGameBoardFactory>()
                .SingleInstance();

            builder.RegisterType<BoardActions>()
                .As<IBoardActions>()
                .SingleInstance();
            

            var container = builder.Build();

            return container.Resolve<IGameEngine>();
        }
    }
}