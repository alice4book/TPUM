using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IShop
    {
        public event EventHandler<PriceChangeEventArgs> PriceChanged;

        List<BookDTO> GetBooks(bool onSale = true);
        bool Sell(List<BookDTO> books);
    }
}
