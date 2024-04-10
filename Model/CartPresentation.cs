using Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CartPresentation
    {
        public ObservableCollection<BookPresentation> Books { get; set; }
        private IShop Shop { get; set; }

        public CartPresentation(ObservableCollection<BookPresentation> books, IShop shop) 
        {
            Books = books;
            Shop = shop;
        }

        public void Add(BookPresentation book)
        {
            Books.Add(book);
        }

        public float Sum()
        {
            float sum = 0.0f;
            foreach (BookPresentation book in Books)
            {
                sum += book.Price;
            }
            return sum;
        }

        public async Task Buy()
        {
            List<BookDTO> bookDTOs = new List<BookDTO>();
            foreach (BookPresentation bookPresentation in Books)
            {
                bookDTOs.Add(Shop.GetBooks().FirstOrDefault(x => x.Id == bookPresentation.Id));
            }
            await Shop.Sell(bookDTOs);
            Books.Clear();
        }
    }
}
