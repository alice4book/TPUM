using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Storage : IStorage
    {
        public List<IBook> Stock { get; }
        public Storage()
        {
            Stock = new List<IBook>();

            Stock.Add(new Book("It", "Clown scary!", "Stephen King", 59.99f, BookType.Horror));
        }

        public void RemoveBooks(List<IBook> books)
        {
            books.ForEach(book => Stock.Remove(book));
        }

        public List<IBook> GetBooksOfType(BookType type) 
        { 
            return Stock.FindAll(book => book.Type == type);
        }

        public List<IBook> GetBooksByAuthor(string author)
        {
            return Stock.FindAll(book => book.Author == author);
        }

        public List<IBook> GetBooksByTitle(string title)
        {
            return Stock.FindAll(book => book.Title == title);
        }

    }
}
