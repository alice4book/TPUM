using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicServer;

namespace ServerPresention
{
    internal class PresentationLayer : IPresentationLayer
    {
        private ILogicLayer _logicLayer;
        private WebSocketConnection _connection;
        

        public PresentationLayer(ILogicLayer logicLayer)
        {
            _logicLayer = logicLayer;
            _logicLayer.onBookRemoved += HandleBookRemoved;
        }

        public static IPresentationLayer CreateDefault()
        {
            return new PresentationLayer(ILogicLayer.CreateDefault());
        }
        public async Task RunServer(int port)
        {
            Console.WriteLine("Server: Start");
            await WebSocketServer.Server(port, ServerConnectionHandler);
        }

        internal void ServerConnectionHandler(WebSocketConnection connection)
        {
            Console.WriteLine("Server: Connected");
            connection.OnClose = () =>
            {
                Console.WriteLine("Server: Closing");
                _connection = null;
            };
            connection.OnMessage = ConnectionMessageHandler;

            _connection = connection;
        }

        public void ConnectionMessageHandler(string message)
        {
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
                            BookInfo.BookInfo bookInfo = new BookInfo.BookInfo
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
                            BookInfo.BookInfo bookInfo = new BookInfo.BookInfo
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
            }
        }

        public async Task SendMessage(string message)
        {
            if (_connection != null)
            {
                Console.WriteLine($"Server: Sending message {message}");
                await _connection.SendAsync(message);
            }
        }

        public void HandleBookRemoved(List<BookDTO> books)
        {

        }
    }
}
