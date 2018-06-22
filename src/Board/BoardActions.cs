using System.Threading;
using System.Threading.Tasks;

namespace WinterIsComing.Server
{
    public class BoardActions : IBoardActions
    {
        private readonly IBroadcastService broadcastService;

        public BoardActions(IBroadcastService broadcastService)
        {
            this.broadcastService = broadcastService;
        }

        public void ScheduleNewZombie(IGameBoard board)
        {
            Task.Factory.StartNew(() => ScheduleNewZombieTask(board));
        }

        public void ScheduleNewZombie(IGameBoard board, int ticksToDelay)
        {
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(ticksToDelay);
                ScheduleNewZombieTask(board);
            });
        }

        private static int zombieNum = 0;

        public void ScheduleNewZombieTask(IGameBoard board)
        {
            zombieNum++;
            var localZombie = zombieNum;

            broadcastService.Broadcast(board, $"Night walker ({localZombie}) will spawn in 10 seconds");
            Thread.Sleep(5000);
            broadcastService.Broadcast(board, $"Night walker ({localZombie}) will spawn in 5 seconds");
            Thread.Sleep(3000);
            broadcastService.Broadcast(board, $"Night walker ({localZombie}) will spawn in 3 seconds");
            Thread.Sleep(1000);
            broadcastService.Broadcast(board, $"Night walker ({localZombie}) will spawn in 2 seconds");
            Thread.Sleep(1000);
            broadcastService.Broadcast(board, $"Night walker ({localZombie}) will spawn in 1 seconds");
            Thread.Sleep(1000);

            board.AddGameObject(new Zombie($"Night-walker ({localZombie})", 0.Random(10), 0));
        }

    }
}