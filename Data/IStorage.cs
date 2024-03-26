using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IStorage
    {
        public List<IBook> Stock { get; }
        public void RemoveBooks(List<IBook> books);
        public List<IBook> GetBooksOfType(BookType type);
        public List<IBook> GetBooksByAuthor(string author);
        public List<IBook> GetBooksByTitle(string title);
        public List<IBook> GetBooksById(List<Guid> Ids);
        public void ChangePrice(Guid id, float newPrice);

        }
}
