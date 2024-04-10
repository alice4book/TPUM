using Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StoragePresentation
    {
        private IShop Shop{get; set;}

        public event EventHandler<PriceChangeEventArgs>? PriceChanged;
        public StoragePresentation(IShop shop)
        {
            Shop = shop;
            shop.PriceChanged += (obj, args) => PriceChanged?.Invoke(this, new PriceChangeEventArgs(args));
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
