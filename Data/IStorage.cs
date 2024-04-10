using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IStorage : IObservable<PriceChangeEventArgs>
    {
        public List<IBook> Stock { get; }
        public void RemoveBooks(List<IBook> books);
        public void AddBook(IBook books);
        public void RemoveAll();
        public List<IBook> GetBooksOfType(BookType type);
        public List<IBook> GetBooksByAuthor(string author);
        public List<IBook> GetBooksByTitle(string title);
        public List<IBook> GetBooksById(List<Guid> Ids);
        public void UpdateAllPrices(List<IBook> newPrices);
        public IBook CreateBook(string title, string description, string author, float price, BookType type);

        public event Action<List<IBook>> onBookRemoved;

    }
}
