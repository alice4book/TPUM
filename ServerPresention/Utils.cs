using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPresention
{
    internal static class Utils
    {
        public static BookInfo ToBookInfo(this LogicServer.BookDTO book)
        {
            return new BookInfo(
                book.Title,
                book.Description,
                book.Author,
                book.Price,
                book.Type.ToString(),
                book.Id
            );
        }
    }
}
