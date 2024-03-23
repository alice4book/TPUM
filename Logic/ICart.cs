using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface ICart
    {
        void AddBook(BookDTO book);
        void RemoveBook(BookDTO book);
    }
}
