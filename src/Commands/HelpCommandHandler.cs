namespace WinterIsComing.Server
{
    class HelpCommandHandler : IGameCommandHandler
    {
        private readonly IBroadcastService broadcastService;

        public HelpCommandHandler(IBroadcastService broadcastService)
        {
            this.broadcastService = broadcastService;
        }

        public void Handle(IGameCommand command)
        {
            Print(command, "Server commands:");
            Print(command, "    START <player_name> <board_name> - join or create a new game");
            Print(command, "    SHOOT <x> <y> - shoot a monster");
            Print(command, "    STATS - prints information about players and their scores");
            Print(command, "    QUIT - quit current game. You can join another one usin START");
            Print(command, "    CHAT <msg> - send a message to players in current game");
            Print(command, "    HELP - print this message");
        }

        private void Print(IGameCommand command, string msg)
        {
            broadcastService.Broadcast(command.ConnectionId, msg);
        }


        public bool CanHandle(IGameCommand command)
        {
            return command is HelpCommand;
        }
    }
}