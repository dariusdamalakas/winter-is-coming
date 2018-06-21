using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace WinterIsComing.Server
{
    interface IGameObject
    {
        int X { get; set; }
        int Y { get; set; }
        string Name { get; set; }
    }

    class Zombie : IGameObject
    {
        public Zombie(string name, int x, int y)
        {
            this.Name = name;
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; }

        public void MoveRandom()
        {
            this.X += 0.Random(3) - 1;
            if (X < 0)
                X = 0;
            if (X > 10)
                X = 10;

            this.Y += 0.Random(2);
            if (Y > 30)
                Y = 30;
        }
    }

    static class RandExtensions
    {
        private static Random rand = new Random();

        public static int Random(this int minValue, int maxValue)
        {
            var res = rand.Next(minValue, maxValue);
            return res;
        }
    }

    public interface IPlayer
    {
        string Name { get; set; }
        string ConnectionId { get; set; }
    }

    public class Player : IPlayer
    {
        public Player(string playerName, string connectionId)
        {
            this.Name = playerName;
            this.ConnectionId = connectionId;
        }

        public string Name { get; set; }
        public string ConnectionId { get; set; }
    }

    class GameBoard : IGameBoard
    {
        private readonly IBroadcastService broadcastService;

        // TODO - thread safety?
        private readonly List<IPlayer> players = new List<IPlayer>();
        private readonly List<IGameObject> gameObjects = new List<IGameObject>();
        public string Name { get; set; }
        Timer timer;

        public GameBoard(string boardName, IBroadcastService broadcastService)
        {
            this.Name = boardName;
            this.broadcastService = broadcastService;
        }

        public bool IsAlreadyJoined(IPlayer player)
        {
            return IsAlreadyJoined(player.ConnectionId);
        }

        public bool IsAlreadyJoined(string connectionId)
        {
            return players.Any(y => y.ConnectionId == connectionId);
        }

        public void Join(IPlayer player)
        {
            this.players.Add(player);
        }

        public void AddGameObject(IGameObject go)
        {
            this.gameObjects.Add(go);
        }

        public void Start()
        {
            if (timer != null)
                throw new Exception("Board already started");
            timer = new Timer(MoveGameObjects, null, 0, 2000);
        }

        public void Stop()
        {
            this.timer?.Dispose();
            this.timer = null;
        }

        private void MoveGameObjects(object state)
        {
            foreach (var go in gameObjects)
            {
                if (go is Zombie zombie)
                {
                    zombie.MoveRandom();
                    broadcastService.Broadcast(this, $"WALK {zombie.Name} {zombie.X} {zombie.Y}");
                }
            }
        }
    }
}