using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataServer;

namespace LogicServer
{
    internal class Shop : IShop
    {
        public event EventHandler<PriceChangeEventArgs> PriceChanged;
        public event Action PriceChangedRefresh;
        private IStorage storage;

        private ISale Sale;
        public Shop(IStorage storage) 
        {
            this.storage = storage;
            Sale = new Sale(storage);
            storage.PriceChange += OnPriceChanged;
        }

        public List<BookDTO> GetBooks(bool onSale = true)
        {
            Tuple<Guid, float> sale = new Tuple<Guid, float>(Guid.Empty, 1f);
            if (onSale)
                sale = Sale.GetSpecialOffer();

            List<BookDTO> availableBooks = new List<BookDTO>();
            
            foreach(IBook book in storage.Stock)
            {
                float price = book.Price;
                if (book.Id.Equals(sale.Item1))
                    price *= sale.Item2;

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
        private void OnPriceChanged(object sender, DataServer.PriceChangeEventArgs e)
        {
            EventHandler<PriceChangeEventArgs> handler = PriceChanged;
            handler?.Invoke(this, new LogicServer.PriceChangeEventArgs(e.Id, e.Price));
            PriceChangedRefresh.Invoke();
        }

    }
}
