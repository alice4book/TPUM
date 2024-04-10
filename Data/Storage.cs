using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Storage : IStorage
    {
        public event Action<List<IBook>> onBookRemoved;
        private readonly object bookLock = new object();
        public event Action? Refresh;
        private HashSet<IObserver<PriceChangeEventArgs>> observers;
        public List<IBook> Stock { get; }

        public Storage()
        {
            Stock = new List<IBook>();
            observers = new HashSet<IObserver<PriceChangeEventArgs>>();
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
                if (book != null)
                {
                    if (!Stock.Contains(book))
                    {
                        Stock.Add(book);
                        Refresh?.Invoke();
                    }
                }
            }
        }
        public void RemoveAll()
        {
            lock (bookLock)
            {
                Stock.Clear();
                Refresh?.Invoke();
            }
        }

        public void RemoveBooks(List<IBook> books)
        {
            lock (bookLock)
            {
                onBookRemoved?.Invoke(books);
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
        public void UpdateAllPrices(List<IBook> newPrices)
        {
            if (newPrices == null)
                return;

            float sale = 0.0f;
            Guid saleID;
            lock (bookLock)
            {
                foreach(var newPrice in newPrices)
                {
                    foreach (var book in Stock)
                    {
                        if (book.Id == newPrice.Id)
                        {
                            if(newPrice.Price < book.Price)
                            {
                                sale = book.Price;
                                saleID = book.Id;
                            }
                            book.Price = newPrice.Price;
                        }
                    }
                }
            }

            foreach (IObserver<PriceChangeEventArgs>? observer in observers)
            {
                observer.OnNext(new PriceChangeEventArgs(saleID, sale));
            }
        }

        public IDisposable Subscribe(IObserver<PriceChangeEventArgs> observer)
        {
            observers.Add(observer);
            return new StorageDisposable(this, observer);
        }

        private class StorageDisposable : IDisposable
        {
            private readonly Storage storage;
            private readonly IObserver<PriceChangeEventArgs> observer;

            public StorageDisposable(Storage storage, IObserver<PriceChangeEventArgs> observer)
            {
                this.storage = storage;
                this.observer = observer;
            }

            public void Dispose()
            {
                storage.UnSubscribe(observer);
            }
        }

        private void UnSubscribe(IObserver<PriceChangeEventArgs> observer)
        {
            observers.Remove(observer);
        }
    }
}
