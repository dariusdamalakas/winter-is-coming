namespace WinterIsComing.Server
{
    public class ChatCommand : IGameCommand
    {
        public ChatCommand(string msg)
        {
            this.Message = msg;
        }

        public string ConnectionId { get; set; }
        public string Message { get; set; }
    }
}