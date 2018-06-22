using System;
using StackExchange.NetGain;

namespace WinterIsComing.Server
{
    public class GameCommandFactory : IGameCommandFactory
    {
        static class Commands
        {
            public const string Start = "START";
            public const string Shoot = "SHOOT";
            public const string PlayerStats = "STATS";
            public const string QuitGame = "QUIT";
            public const string ChatCommand = "CHAT";
            public const string HelpCommand = "HELP";
        }

        public IGameCommand  BuildFrom(string msg)
        {
            var splt = msg.Split(' ');
            //if (splt.Length < 2)
            //    throw new InvalidGameCommandException("Command should take at least 1 param");

            if (splt[0].StartsWith(Commands.Start))
                return BuildStartGameCommand(splt);
            if (splt[0].StartsWith(Commands.Shoot))
                return BuildShootGameCommand(splt);
            if (splt[0].StartsWith(Commands.PlayerStats))
                return BuildStatsCommand(splt);
            if (splt[0].StartsWith(Commands.QuitGame))
                return BuildQuitGameCommand(splt);
            if (splt[0].StartsWith(Commands.ChatCommand))
                return BuildChatCommand(msg);
            if (splt[0].StartsWith(Commands.HelpCommand))
                return BuildHelpCommand(splt);
            throw new InvalidGameCommandException($"Unknown command");
        }

        private IGameCommand BuildHelpCommand(string[] splt)
        {
            return new HelpCommand();
        }

        private IGameCommand BuildChatCommand(string msg)
        {
            return new ChatCommand(msg.Substring(Commands.Start.Length).Trim());
        }

        private IGameCommand BuildQuitGameCommand(string[] splt)
        {
            return new QuitCommand();
        }

        private IGameCommand BuildStatsCommand(string[] splt)
        {
            return new PlayerStatsCommand();
        }

        private IGameCommand BuildShootGameCommand(string[] split)
        {
            return new ShootCommand()
            {
                X = GetAsIntOrFail(split, 1),
                Y = GetAsIntOrFail(split, 2),
            };
        }

        private int GetAsIntOrFail(string[] split, int p1)
        {
            if (split.Length < 3)
                throw new InvalidGameCommandException("Shoot command takes 2 args");
            var s = split[p1];
            if (int.TryParse(s, out var result) == false)
            {
                throw new InvalidGameCommandException($"Unrecognized game coordinate: '{s}'");
            }
            return result;
        }

        private IGameCommand BuildStartGameCommand(string[] split)
        {
            if (split.Length < 2)
                throw new InvalidGameCommandException("Start command takes minimum 2 args");
            if (split.Length > 3)
                throw new InvalidGameCommandException("Start command takes maximum 3 args");
            return new StartGameCommand()
            {
                PlayerName = split[1],
                BoardName = split.Length > 2 ? split[2] : null
            };
        }
    }
}