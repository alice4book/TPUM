using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClientApi;

namespace Data
{
    internal class DataLayer : IDataLayer
    {
        private IStorage storage;
        public event Action<string> onConnectionMessage;
        private WebSocketConnection connection = null;
        private SynchronizationContext context = SynchronizationContext.Current;

        public override IStorage Storage { get => storage; set => storage = value; }

        internal DataLayer(IStorage storage = default) 
        {
            Storage = storage ?? new Storage();
            context = SynchronizationContext.Current;
            Storage.onBookRemoved += SendRemovingMessage;
        }

        public override async Task Connect(Uri uri)
        {
            try
            {
                connection = await WebSocketClient.Connect(uri, log => { });
                if (connection != null)
                {
                    connection.OnMessage = ConnectionMessageHandler;
                    await SendMessage("GetBooks"); 
                }
            }
            catch
            {
                connection = null;
            }
        }

        public override async Task SendMessage(string message)
        {
            if (connection != null)
            {
                Console.WriteLine($"Client: Sending message {message}");
                await connection.SendAsync(message);
            }
        }


        private async void SendRemovingMessage(List<IBook> books)
        {
            string response = $"RemoveBooks;{books.Count}";
            foreach (IBook book in books)
            {
                BookInfo bookInfo = new BookInfo
                {
                    Title = book.Title,
                    Description = book.Description,
                    Author = book.Author,
                    Price = book.Price,
                    Type = book.Type.ToString(),
                    Id = book.Id
                };
                string bookstr = $";{Serializer.SerializeBook(bookInfo)}";
                response += bookstr;
            }

            await SendMessage(response);
        }

        public override void ConnectionMessageHandler(string message)
        {
            context.Post((obj) =>
            {
                ProcessMessage(message);
                onConnectionMessage?.Invoke(message);
            }, null);
        }

        private bool ProcessMessage(string message)
        {
            string[] operands = message.Split(';');
            if (operands.Length < 1)
            {
                return false;
            }

            string op = operands[0];
            switch (op)
            {
                case "SendBooks":
                    {
                        if (operands.Length < 2)
                        {
                            return false;
                        }
                        Storage.RemoveAll();
                        List<IBook> books = new List<IBook>();
                        int count = Int32.Parse(operands[1]);
                        for (int idx = 0; idx < count; ++idx)
                        {
                            int offset = 2 + idx;
                            IBook book = Serializer.DeserializeBook(operands[offset]);
                            Storage.AddBook(book);
                            books.Add(book);
                        }
                        Storage.UpdateAllPrices(books);
                        break;
                    }
            }
            return true;
        }
        private IBook BookFromArgs(string[] args, int offset)
        {
            Guid id = Guid.Parse(args[offset]);
            string title = args[offset + 1];
            string description = args[offset + 2];
            string author = args[offset + 3];
            float price = float.Parse(args[offset + 4]);
            BookType type = (BookType) Enum.Parse(typeof(BookType),args[offset + 5], true);
            return new Book(id,title, description, author, price, type);
        }
    }
}
