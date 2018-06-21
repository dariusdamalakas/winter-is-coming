using System;

namespace WinterIsComing.Server
{
    public class InvalidGameCommandException : Exception
    {
        public InvalidGameCommandException(string message) : base(message)
        {
        }
    }
}