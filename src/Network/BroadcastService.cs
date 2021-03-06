﻿using System;
using System.Net;
using StackExchange.NetGain;
using StackExchange.NetGain.WebSockets;

namespace WinterIsComing.Server
{
    class GameNework : IGameNetwork, IDisposable
    {
        private TcpServer server;

        public void Broadcast(string connectionId, string message)
        {
            this.server.Broadcast(message, y => y.Id.ToString() == connectionId);
        }

        public void Broadcast(IGameBoard board, string message)
        {
            this.server.Broadcast(message, y=> HasActiveGame(y, board));
        }

        public void BroadcastAll(string message)
        {
            this.server.Broadcast(message, y => true);
        }

        public void BroadcastExcept(IGameBoard board, string message, Func<string, bool> connectionFilter)
        {
            this.server.Broadcast(message, y => HasActiveGame(y, board) && connectionFilter(y.Id.ToString()) == false);
        }

        private bool HasActiveGame(Connection connection, IGameBoard board)
        {
            return board.IsAlreadyJoined(connection.Id.ToString());
        }

        public void Stop()
        {
            this.server.Stop();
        }

        public void Start()
        {
            var endpoint = new IPEndPoint(IPAddress.Any, 6002);
            server = new TcpServer();
            server.ProtocolFactory = WebSocketsSelectorProcessor.Default;
            server.ConnectionTimeoutSeconds = 60;

            server.Start("abc", endpoint);
        }

        public void SetOnMessageReceived(Action<Message> onMessageReceived)
        {
            server.Received += onMessageReceived;
        }

        public void Dispose()
        {
            ((IDisposable) server)?.Dispose();
        }
    }
}