using StackExchange.NetGain;

namespace WinterIsComing.Server
{
    public interface IGameLoopHandler
    {
        void GameLoop(Message msg);
    }
}