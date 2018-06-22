using System;

namespace WinterIsComing.Server
{
    public interface IBroadcastService
    {
        void Broadcast(string connectionId, string message);
        void Broadcast(IGameBoard gameBoard, string message);
        void BroadcastAll(string message);

        /// <summary>
        /// Broadcast to all in a game except some specific connections
        /// </summary>
        /// <param name="board"></param>
        /// <param name="message"></param>
        /// <param name="connectionFilter">return true which connections should be skipped.
        /// Takes connectionId as param. </param>
        void BroadcastExcept(IGameBoard board, string message, Func<string, bool> connectionFilter);
    }
}