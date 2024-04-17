using ConnectApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{

    public interface IConnectionService
    {
        public SynchronizationContext Context { get; }
        public WebSocketConnection Connection { get; }

        public Task Connect(Uri peerUri);
        public Task Disconnect();

        public bool IsConnected();

        public Task SendMessage(string message);

    }
    internal class ConnectionService : IConnectionService
    {

        private WebSocketConnection connection = null;
        public  SynchronizationContext context = SynchronizationContext.Current;

        public SynchronizationContext Context => context;

        public WebSocketConnection Connection => connection;

        public async Task Connect(Uri uri)
        {
            try
            {
                connection = await WebSocketClient.Connect(uri, log => { });
            }
            catch
            {
                connection = null;
            }
        }

        public async Task SendMessage(string message)
        {
            if (connection != null)
            {
                Console.WriteLine($"Client: Sending message {message}");
                await connection.SendAsync(message);
            }
        }

        public async Task Disconnect()
        {
            if (connection != null)
            {
                await connection.DisconnectAsync();
            }
        }

        public bool IsConnected()
        {
            return connection != null;
        }

    }
}
