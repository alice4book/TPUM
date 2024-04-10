using Data;
using System.Diagnostics;
using static System.Reflection.Metadata.BlobBuilder;

namespace LogicTest
{
    public class DataMock : IDataLayer
    {
        private readonly IStorage storage = new StorageMock();
        public override IStorage Storage
        {
            get { return storage; }
            set { throw new NotImplementedException(); }
        }

        public override Task Connect(Uri uri)
        {
            throw new NotImplementedException();
        }

        public override void ConnectionMessageHandler(string message)
        {
            throw new NotImplementedException();
        }

        public override Task SendMessage(string message)
        {
            throw new NotImplementedException();
        }
    }

    public class BookMock : IBook
    {
        public string Title { get; }

        public string Description { get; }

        public string Author { get; }

        private float price;
        public float Price {
            get => price;
            set
            {
                if (value == price)
                    return;

                price = value;

            }
        }

        public BookType Type { get; }

        public Guid Id { get; }
    }

    public class StorageMock : IStorage
    {
        public List<IBook> Stock { get; }

        public event Action<List<IBook>> onBookRemoved;
        public event Action<IBook> onBookAdded;
        public event Action Refresh;

        public StorageMock()
        {
            Stock = new List<IBook>
            {
                new MockBook("It", "Clown scary!", "Stephen King", 59.99f, BookType.Horror),
                new MockBook("This", "Clown scary!", "Stephen King", 19.99f, BookType.Romance)
            };
        }

        public void ChangePrice(Guid id, float newPrice)
        {
            foreach (var book in Stock) 
            {
                if (book.Id == id)
                {
                    book.Price = newPrice;
                    break;
                }
            }
        }

        public IBook CreateBook(string title, string description, string author, float price, BookType type)
        {
            return new MockBook(title, description, author, price, type);
        }

        public List<IBook> GetBooksByAuthor(string author)
        {
            return Stock.FindAll(book => book.Author == author);
        }

        public List<IBook> GetBooksById(List<Guid> Ids)
        {
            List<IBook> books = new List<IBook>();
            foreach (Guid id in Ids)
            {
                List<IBook> tmp = Stock.FindAll(x => x.Id == id);
                if (tmp.Count > 0)
                    books.AddRange(tmp);
            }
            return books;
        }

        public List<IBook> GetBooksByTitle(string title)
        {
            return Stock.FindAll(book => book.Title == title);
        }

        public List<IBook> GetBooksOfType(BookType type)
        {
            return Stock.FindAll(book => book.Type == type);
        }

        public void RemoveBooks(List<IBook> books)
        {
            books.ForEach(book => Stock.Remove(book));
        }

        public void AddBook(IBook books)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public void UpdateAllPrices(List<IBook> newPrices)
        {
            throw new NotImplementedException();
        }

        public IDisposable Subscribe(IObserver<PriceChangeEventArgs> observer)
        {
            throw new NotImplementedException();
        }
    }

    public class MockBook : IBook
    {
        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }
        public string Author { get; }
        public float Price
        {
            get => price;
            set
            {
                if (value == price)
                    return;

                price = value;

            }
        }
        private float price;
        public BookType Type { get; }

        public MockBook(string title, string description, string author, float price, BookType type)
        {
            Title = title;
            Description = description;
            Author = author;
            Price = price;
            Id = Guid.NewGuid();
            Type = type;
        }
    }
}
