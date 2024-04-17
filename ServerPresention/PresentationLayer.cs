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
            Console.WriteLine($"Sending books...");
            SendBooksResponse serverResponse = new SendBooksResponse();
            List<BookDTO> books = _logicLayer.Shop.GetBooks();
            serverResponse.Books = books.Select(x => x.ToBookInfo()).ToArray();

            Serializer serializer = Serializer.Create();
            string response = serializer.Serialize(serverResponse);
            Console.WriteLine(response);
            Task task = Task.Run(async () => await SendMessage(response));
        }

        public static IPresentationLayer CreateDefault()
        {
            return new PresentationLayer(ILogicLayer.CreateDefault());
        }

        internal void ServerConnectionHandler(WebSocketConnection connection)
        {
            Console.WriteLine("Server: Connected");
            connection.OnClose = () =>
            {
                Console.WriteLine("Server: Closing");
                _connection = null;
            };
            _connection = connection;
            connection.OnMessage = ConnectionMessageHandler;

            Serializer serializer = Serializer.Create();
            SendBooksResponse serverResponse = new SendBooksResponse();
            List<BookDTO> books = _logicLayer.Shop.GetBooks();
            serverResponse.Books = books.Select(x => x.ToBookInfo()).ToArray();

            string response = serializer.Serialize(serverResponse);
            Console.WriteLine(response);
            Task task = Task.Run(async () => await SendMessage(response));

        }
        public async Task RunServer(int port)
        {
            Console.WriteLine("Server: Start");
            await WebSocketServer.Server(port, ServerConnectionHandler);
        }

        public void ConnectionMessageHandler(string message)
        {
            if (!_connection.IsConnected)
            {
                return;
            }
            Console.WriteLine($"New message: {message}");
            Serializer serializer = Serializer.Create();
            if (serializer.GetCommandHeader(message) == SellBookCommand.StaticHeader)
            {
                
                SellBookCommand sellBookCommand = serializer.Deserialize<SellBookCommand>(message);/*
                try
                {
                    _logicLayer.Shop.Sell(sellBookCommand.BookIDs);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Exception \"{exception.Message}\" caught during selling books");
                }*/
                //Console.WriteLine($"Send: {transactionMessage}");
                //await webSocketConnection.SendAsync(transactionMessage);
            }
            else if (serializer.GetCommandHeader(message) == GetBooksCommand.StaticHeader)
            {
                SendBooksResponse serverResponse = new SendBooksResponse();
                List<BookDTO> books = _logicLayer.Shop.GetBooks();
                serverResponse.Books = books.Select(x => x.ToBookInfo()).ToArray();

                string response = serializer.Serialize(serverResponse);
                Console.WriteLine(response);
                Task task = Task.Run(async () => await SendMessage(response));
            }
            /*
            Console.WriteLine($"Server: Received message {message}");
            string[] operands = message.Split(';');
            if (operands.Length < 1)
            {
                return;
            }
            string op = operands[0];
            switch (op)
            {
                case "GetBooks":
                    {
                        List<BookDTO> books = _logicLayer.Shop.GetBooks();
                        string response = $"SendBooks;{books.Count}";
                        foreach (BookDTO book in books)
                        {
                            BookInfo bookInfo = new BookInfo
                            {
                                Title = book.Title,
                                Description = book.Description,
                                Author = book.Author,
                                Price = book.Price,
                                Type = book.Type,
                                Id = book.Id
                            };
                            string bookstr = $";{Serializer.SerializeBook(bookInfo)}";
                            response += bookstr;
                        }

                        SendMessage(response);
                        break;
                    }
                case "RemoveBooks":
                    {

                        if (operands.Length < 2)
                        {
                            break;
                        }
                        List<BookDTO> booksToRemove = new List<BookDTO>();
                        int count = Int32.Parse(operands[1]);
                        for (int idx = 0; idx < count; ++idx)
                        {
                            int offset = 2 + idx;
                            BookDTO book = Serializer.DeserializeBook(operands[offset]);
                            booksToRemove.Add(book);
                        }
                        Console.WriteLine($"Server: Finished removing books "+ _logicLayer.Shop.Sell(booksToRemove));
                        List<BookDTO> books = _logicLayer.Shop.GetBooks();
                        string response = $"SendBooks;{books.Count}";
                        foreach (BookDTO book in books)
                        {
                            BookInfo bookInfo = new BookInfo
                            {
                                Title = book.Title,
                                Description = book.Description,
                                Author = book.Author,
                                Price = book.Price,
                                Type = book.Type,
                                Id = book.Id
                            };
                            string bookstr = $";{Serializer.SerializeBook(bookInfo)}";
                            response += bookstr;
                        }

                        SendMessage(response);
                        break;
                    }
            }*/
        }

        public async Task SendMessage(string message)
        {
            if (_connection != null)
            {
                Console.WriteLine($"Server: Sending message {message}");
                await _connection.SendAsync(message);
            }
        }

    }
}
