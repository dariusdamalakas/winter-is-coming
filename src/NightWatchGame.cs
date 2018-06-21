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

            var container = builder.Build();

            return container.Resolve<IGameEngine>();
        }
    }
}