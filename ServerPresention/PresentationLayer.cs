using LogicServer;
using ConnectApi;

namespace ServerPresention
{
    internal class PresentationLayer : IPresentationLayer
    {
        private ILogicLayer _logicLayer;
        private WebSocketConnection _connection;
        

        public PresentationLayer(ILogicLayer logicLayer)
        {
            _logicLayer = logicLayer;
            _logicLayer.Shop.PriceChangedRefresh += OnPriceChanged;
        }

        void OnPriceChanged()
        {
            if (_connection == null )
            {
                return;
            }
            SendBooksResponse serverResponse = new SendBooksResponse();
            List<BookDTO> books = _logicLayer.Shop.GetBooks();
            serverResponse.Books = books.Select(x => x.ToBookInfo()).ToArray();

            Serializer serializer = Serializer.Create();
            string response = serializer.Serialize(serverResponse);
            Task task = Task.Run(async () => await SendMessage(response));
        }

        public static IPresentationLayer CreateDefault()
        {
            return new PresentationLayer(ILogicLayer.CreateDefault());
        }

        internal void ServerConnectionHandler(WebSocketConnection connection)
        {
            //Console.WriteLine("Server: Connected");
            connection.OnClose = () =>
            {
                //Console.WriteLine("Server: Closing");
                _connection = null;
            };
            _connection = connection;
            connection.OnMessage = ConnectionMessageHandler;
        }
        public async Task RunServer(int port)
        {
            //Console.WriteLine("Server: Start");
            await WebSocketServer.Server(port, ServerConnectionHandler);
        }

        public void ConnectionMessageHandler(string message)
        {
            if(_connection == null) { return; }
            //Console.WriteLine($"New message: {message}");
            Serializer serializer = Serializer.Create();
            if (serializer.GetCommandHeader(message) == SellBookCommand.StaticHeader)
            {
                SellBookCommand sellBookCommand = serializer.Deserialize<SellBookCommand>(message);
                List<Guid> bookIds = new List<Guid>();
                if (sellBookCommand == null) { return; }
                foreach(Guid book in sellBookCommand.BookIDs) { bookIds.Add(book); }
                _logicLayer.Shop.Sell(bookIds);
            }
            else if (serializer.GetCommandHeader(message) == GetBooksCommand.StaticHeader)
            {
                SendBooksResponse serverResponse = new SendBooksResponse();
                List<BookDTO> books = _logicLayer.Shop.GetBooks();
                serverResponse.Books = books.Select(x => x.ToBookInfo()).ToArray();

                string response = serializer.Serialize(serverResponse);
                //Console.WriteLine(response);
                Task task = Task.Run(async () => await SendMessage(response));
            }
        }

        public async Task SendMessage(string message)
        {
            if (_connection != null)
            {
                //Console.WriteLine($"Server: Sending message {message}");
                await _connection.SendAsync(message);
            }
        }

    }
}
