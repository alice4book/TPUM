using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data
{
    internal static class Utils
    {
        public static BookType FromStringToType(string typeAsString)
        {
            return (BookType)Enum.Parse(typeof(BookType), typeAsString);
        }

        public static string ToString(this BookType typeAsString)
        {
            return Enum.GetName(typeof(BookType), typeAsString) ?? throw new InvalidOperationException();
        }

        public static IBook ToBook(this BookInfo bookInfo)
        {
            return new Book(
                bookInfo.Id,
                bookInfo.Title,
                bookInfo.Description,
                bookInfo.Author,
                bookInfo.Price,
                FromStringToType(bookInfo.Type)
            );
        }
    }
}
