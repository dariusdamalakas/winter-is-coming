﻿using System;
using StackExchange.NetGain;

namespace WinterIsComing.Server
{
    public class GameCommandFactory : IGameCommandFactory
    {
        static class Commands
        {
            public const string Start = "START";
            public const string Shoot = "SHOOT";
        }

        public IGameCommand  BuildFrom(string msg)
        {
            var splt = msg.Split(' ');
            if (splt.Length < 2)
                throw new InvalidGameCommandException("Command should take at least 1 param");

            if (splt[0].StartsWith(Commands.Start))
                return BuildStartGameCommand(splt);
            if (splt[0].StartsWith(Commands.Shoot))
                return BuildShootGameCommand(splt);
            throw new InvalidGameCommandException($"Unknown command");
        }

        private IGameCommand BuildShootGameCommand(string[] split)
        {
            return new PlayerShootCommand()
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
            if (int.TryParse(s, out var result))
            {
                throw new InvalidGameCommandException($"Unrecognized game coordinate: {s}");
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