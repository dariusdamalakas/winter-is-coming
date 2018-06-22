namespace WinterIsComing.Server
{
    interface IBoardActions
    {
        void ScheduleNewZombie(IGameBoard board);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="ticksToDelay">will increase delay by x ticks</param>
        void ScheduleNewZombie(IGameBoard board, int ticksToDelay);
    }
}