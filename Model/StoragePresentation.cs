using Data;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StoragePresentation
    {
        private IShop Shop{get; set;}

        public StoragePresentation(IShop shop)
        {
            Shop = shop;
        }
        public List<BookPresentation> GetBooks()
        {
            List<BookPresentation> books = new List<BookPresentation>();
            foreach (BookDTO book in Shop.GetBooks())
            {
                books.Add(new BookPresentation(book.Title, book.Description, book.Author, book.Price, book.Type, book.Id));
            }

            return books;
        }
    }
}
