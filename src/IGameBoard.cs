using System.Collections;
using System.Collections.Generic;

namespace WinterIsComing.Server
{
    public interface IGameBoard
    {
        string Name { get; set; }
        IList<IPlayer> Players { get; }
        bool IsAlreadyJoined(IPlayer player);
        bool IsAlreadyJoined(string connectionId);
        void Join(IPlayer player);
        IPlayer FindPlayer(string connectionId);
        void RemovePlayer(string connectionId);

        void Start();
        void Stop();

        IGameObject ObjectAt(int x, int y);
        void AddGameObject(IGameObject zombie);
        void RemoveGameObject(IGameObject gameObject);
    }
}