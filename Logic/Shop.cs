using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class Shop : IShop
    {
        private IStorage storage;

        public Shop(IStorage storage) 
        {
            this.storage = storage;
        }

        public List<BookDTO> GetBooks()
        {
            List<BookDTO> availableBooks = new List<BookDTO>();
            foreach(IBook book in storage.Stock)
            {
                availableBooks.Add(new BookDTO 
                { 
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    Price = book.Price,
                    Type = book.Type.ToString(),
                    Id = book.Id
                });
            }

            return availableBooks;
        }

        public void Sell(List<BookDTO> books)
        {
            List<Guid> bookIds = new List<Guid>();
            foreach (BookDTO book in books)
                bookIds.Add(book.Id);
            List<IBook> booksDataLayer = storage.GetBooksById(bookIds);
            storage.RemoveBooks(booksDataLayer);
        }
    }
}
