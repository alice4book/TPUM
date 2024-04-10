using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    internal class Shop : IShop, IObserver<Data.PriceChangeEventArgs>
    {
        public event EventHandler<PriceChangeEventArgs>? PriceChanged;
        private IDisposable StoragSubscriptionHandle;
        private IStorage storage;

        public Shop(IStorage storage) 
        {
            this.storage = storage;
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

        public async Task Sell(List<BookDTO> books)
        {
            Debug.Assert(books.Count > 0);
            List<Guid> bookIds = new List<Guid>();
            foreach (BookDTO book in books)
                bookIds.Add(book.Id);
            List<IBook> booksDataLayer = storage.GetBooksById(bookIds);

            await storage.RemoveBooks(booksDataLayer);
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
