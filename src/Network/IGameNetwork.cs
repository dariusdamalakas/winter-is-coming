using System;
using StackExchange.NetGain;

namespace WinterIsComing.Server
{
    public interface IGameNetwork: IBroadcastService
    {
        void Stop();
        void Start();
        void SetOnMessageReceived(Action<Message> onMessageReceived);
    }
}