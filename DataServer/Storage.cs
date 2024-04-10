using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServer
{
    internal class Storage : IStorage
    {
        public event EventHandler<PriceChangeEventArgs> PriceChange;
        private readonly object bookLock = new object();
        public List<IBook> Stock { get; }

        public Storage()
        {
            Stock = new List<IBook>();

            Stock.Add(CreateBook("It", "Clown scary!", "Stephen King", 59.99f, BookType.Horror));
            Stock.Add(CreateBook("This", "Clown scary!", "Stephen King", 19.99f, BookType.Romance));
            Stock.Add(CreateBook("That", "Clown scary!", "Stephen King", 29.99f, BookType.Horror));
        }

        public IBook CreateBook(string title, string description, string author, float price, BookType type)
        {
            lock(bookLock) {
                return new Book(title, description, author, price, type); 
            }
        }

        public void AddBook(IBook book)
        {
            lock(bookLock)
            {
                if (book == null)
                {
                    if (!Stock.Contains(book))
                    {
                        Stock.Add(book);
                    }
                }
            }
        }

        public void RemoveBooks(List<IBook> books)
        {
            lock (bookLock)
            {
                //books.ForEach(book => Stock.Remove(book));
                foreach (IBook book in books)
                {
                    if (Stock.Remove(book))
                    {
                        //onBookRemoved?.Invoke(books);
                    }
                }

            }
        }

        public List<IBook> GetBooksOfType(BookType type) 
        {
            lock (bookLock)
            {
                return Stock.FindAll(book => book.Type == type);
            }
        }

        public List<IBook> GetBooksByAuthor(string author)
        {
            lock (bookLock)
            {
                return Stock.FindAll(book => book.Author == author);
            }
        }

        public List<IBook> GetBooksByTitle(string title)
        {
            lock (bookLock)
            {
                return Stock.FindAll(book => book.Title == title);
            }
        }

        public void ChangePrice(Guid id, float newPrice)
        {
            lock (bookLock)
            {
                IBook book = Stock.Find(x => x.Id.Equals(id));
                if (book == null)
                    return;
                if (Math.Abs(newPrice - book.Price) < 0.01f)
                    return;
                book.Price = newPrice;
                OnPriceChanged(book.Id, book.Price);
            }
        }

        private void OnPriceChanged(Guid id, float price)
        {
            EventHandler<PriceChangeEventArgs> handler = PriceChange;
            handler?.Invoke(this, new PriceChangeEventArgs(id, price));
        }
        public List<IBook> GetBooksById(List<Guid> Ids)
        {
            List<IBook> books = new List<IBook>();
            lock (bookLock)
            {
                foreach (Guid id in Ids)
                {
                    List<IBook> tmp = Stock.FindAll(x => x.Id == id);
                    if (tmp.Count > 0)
                        books.AddRange(tmp);
                }
            }
            return books;
        }

    }
}
