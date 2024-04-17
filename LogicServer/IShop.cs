using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicServer
{
    public interface IShop
    {
        public event EventHandler<PriceChangeEventArgs> PriceChanged;
        public event Action PriceChangedRefresh;

        List<BookDTO> GetBooks(bool onSale = true);
        bool Sell(List<Guid> books);
    }
}
