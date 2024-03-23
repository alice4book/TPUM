using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class Cart : ICart
    {
        private List<BookDTO> Books;
        public float CartValue { get; private set; }

        public Cart()
        { 
            Books = new List<BookDTO>();
            CartValue = 0.0f;
        }

        public void AddBook(BookDTO book)
        {
            Books.Add(book);
            CartValue += book.Price;
        }

        public void RemoveBook(BookDTO book)
        {
            Books.Remove(book);
            CartValue -= book.Price;
        }
    }
}
