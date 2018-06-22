using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace WinterIsComing.Server
{
    class GameBoard : IGameBoard
    {
        private readonly IBroadcastService broadcastService;
        private readonly IBoardActions boardActions;

        // TODO - thread safety?
        private readonly List<IPlayer> players = new List<IPlayer>();
        private readonly List<IGameObject> gameObjects = new List<IGameObject>();
        public string Name { get; set; }
        public IList<IPlayer> Players => this.players;
        private Timer timer;

        public GameBoard(string boardName, IBroadcastService broadcastService, IBoardActions boardActions)
        {
            this.Name = boardName;
            this.broadcastService = broadcastService;
            this.boardActions = boardActions;
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

        public IPlayer FindPlayer(string connectionId)
        {
            return players.SingleOrDefault(y => y.ConnectionId == connectionId);
        }

        public void RemovePlayer(string connectionId)
        {
            this.players.RemoveAll(y => y.ConnectionId == connectionId);
        }

        public IGameObject ObjectAt(int x, int y)
        {
            foreach (var go in gameObjects)
            {
                if (go.X == x && go.Y == y)
                    return go;
            }
            return null;
        }

        public void AddGameObject(IGameObject go)
        {
            this.gameObjects.Add(go);
        }

        public void RemoveGameObject(IGameObject gameObject)
        {
            this.gameObjects.Remove(gameObject);
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

        public IList<IGameObject> GameObjects => this.gameObjects;

        private void MoveGameObjects(object state)
        {
            var unitsToRemove = new List<IGameObject>();

            foreach (var go in gameObjects)
            {
                if (go is Zombie zombie)
                {
                    zombie.MoveRandom();
                    broadcastService.Broadcast(this, $"[{this.Name}] WALK {zombie.Name} {zombie.X} {zombie.Y}");

                    if (zombie.Y >= 20)
                    {
                        EndGameAndRespawn();
                        unitsToRemove.Add(go);
                    }
                }
            }

            foreach (var go in unitsToRemove)
            {
                this.gameObjects.Remove(go);
            }
        }

        private void EndGameAndRespawn()
        {
            broadcastService.Broadcast(this, $"White walker has breached the wall. You all die.");
            this.players.ForEach(y => y.Score --);
            this.boardActions.ScheduleNewZombie(this);

        }
    }
}