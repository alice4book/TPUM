using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IShop
    {
        List<BookDTO> GetBooks();
        bool Sell(List<BookDTO> books);
    }
}
