namespace WinterIsComing.Server
{
    class ChatCommandHandler : IGameCommandHandler
    {
        private readonly IBroadcastService broadcastService;
        private readonly IBoardManager boardManager;

        public ChatCommandHandler(IBroadcastService broadcastService,
            IBoardManager boardManager)
        {
            this.broadcastService = broadcastService;
            this.boardManager = boardManager;
        }

        public void Handle(IGameCommand command)
        {
            var chatMsg = command as ChatCommand;
            var board = this.boardManager.FindByConnectionId(command.ConnectionId);
            if (board == null)
            {
                this.broadcastService.Broadcast(command.ConnectionId, "No active game");
                return;
            }

            var player = board.FindPlayer(command.ConnectionId);
            var msg = $"{player.Name}> {chatMsg?.Message}";
            this.broadcastService.BroadcastExcept(board, msg, s => s == command.ConnectionId);
        }

        public bool CanHandle(IGameCommand command)
        {
            return command is ChatCommand;
        }
    }
}