using System;
using System.Collections.Generic;
using Data;

namespace Logic
{
    internal class Shop : IShop, IObserver<Data.PriceChangeEventArgs>
    {
        public event Action? Refresh;
        public event EventHandler<PriceChangeEventArgs>? PriceChanged;
        private IDisposable StoragSubscriptionHandle;
        private IStorage storage;

        public Shop(IStorage storage) 
        {
            this.storage = storage;
            //storage.Refresh += RefreshBooks;
            StoragSubscriptionHandle = storage.Subscribe(this);
        }

        public List<BookDTO> GetBooks(bool onSale = true)
        {
            List<BookDTO> availableBooks = new List<BookDTO>();
            
            foreach(IBook book in storage.Stock)
            {
                availableBooks.Add(new BookDTO 
                { 
                    Title = book.Title,
                    Description = book.Description,
                    Author = book.Author,
                    Price = book.Price,
                    Type = book.Type.ToString(),
                    Id = book.Id
                });
            }

            return availableBooks;
        }

        public bool Sell(List<BookDTO> books)
        {
            List<Guid> bookIds = new List<Guid>();
            foreach (BookDTO book in books)
                bookIds.Add(book.Id);
            List<IBook> booksDataLayer = storage.GetBooksById(bookIds);
            storage.RemoveBooks(booksDataLayer);
            return true;
        }
        private void RefreshBooks()
        {
            Refresh?.Invoke();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(Data.PriceChangeEventArgs value)
        {
            PriceChanged?.Invoke(this, new PriceChangeEventArgs(value));
        }
    }
}
