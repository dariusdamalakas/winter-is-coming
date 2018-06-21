using StackExchange.NetGain;

namespace WinterIsComing.Server
{
    public interface IGameCommandFactory
    {
        IGameCommand BuildFrom(string msg);
    }
}