using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConnectApi;

namespace Data
{
    internal class DataLayer : IDataLayer
    {
        private IStorage storage;
        public event Action<string>? onMessage;
        public override event Action<string> onConnectionMessage;
        IConnectionService connectionService;
        public override IStorage Storage { get => storage; set => storage = value; }

        internal DataLayer(IStorage storage = default) 
        {
            connectionService = new ConnectionService();
            Storage = storage ?? new Storage(connectionService);
        }

        public override async Task Connect(Uri uri)
        {
            await connectionService.Connect(uri);
            if (connectionService.IsConnected())
            {

                connectionService.Connection.OnMessage = ConnectionMessageHandler;
                Task task = Task.Run(async () => await RequestBooks());
                
            }
        }

        public async Task RequestBooks()
        {
            Serializer serializer = Serializer.Create();
            Debug.WriteLine("RequestBooks");
            GetBooksCommand getBooksCommand = new GetBooksCommand { Header = ServerStatics.GetBooksCommandHeader };
            await connectionService.SendMessage(serializer.Serialize(getBooksCommand));
        }

        private void OnMessage(string message)
        {
            Serializer serializer = Serializer.Create();

            if (serializer.GetResponseHeader(message) == ServerStatics.SendsBooksResponseHeader)
            {
                SendBooksResponse response = serializer.Deserialize<SendBooksResponse>(message);
                UpdateBooks(response);
            }
        }


        private void UpdateBooks(SendBooksResponse response)
        {
            if (response.Books == null)
                return;
            List<IBook> newBooks = new List<IBook>();
            foreach (var book in response.Books)
            {
                newBooks.Add(book.ToBook());
            }
            storage.UpdateAllPrices(newBooks);
        }

        public override void ConnectionMessageHandler(string message)
        {
            connectionService.Context.Post((obj) =>
            {
                OnMessage(message);
                onConnectionMessage?.Invoke(message);
            }, null);
        }
    }
}
