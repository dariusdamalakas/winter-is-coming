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

        public void ScheduleNewZombieTask(IGameBoard board)
        {
            broadcastService.Broadcast(board, "Night walker will spawn in 10 seconds");
            Thread.Sleep(5000);
            broadcastService.Broadcast(board, "Night walker will spawn in 5 seconds");
            Thread.Sleep(3000);
            broadcastService.Broadcast(board, "Night walker will spawn in 3 seconds");
            Thread.Sleep(1000);
            broadcastService.Broadcast(board, "Night walker will spawn in 2 seconds");
            Thread.Sleep(1000);
            broadcastService.Broadcast(board, "Night walker will spawn in 1 seconds");
            Thread.Sleep(1000);
            board.AddGameObject(new Zombie("Night-walker", 0.Random(10), 0));
        }

    }
}