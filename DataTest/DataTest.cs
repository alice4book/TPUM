using Data;
using System.Diagnostics;

namespace DataTest
{
    [TestClass]
    public class DataTest
    {
        private IDataLayer PrepareData()
        {
            IDataLayer data = IDataLayer.Create();
            return data;
        }
        [TestMethod]
        public void AddRemoveBookTest()
        {
            IDataLayer data = PrepareData();
            string title = "Hello";
            string description = "World";
            string author = "Author";
            float price = 200;
            BookType type = BookType.Horror;

            IBook book = data.Storage.CreateBook(title, description, author, price, type);
            Assert.AreEqual(title, book.Title);
            Assert.AreEqual(description, book.Description);
            Assert.AreEqual(author, book.Author);
            Assert.AreEqual(price, book.Price);
            Assert.AreEqual(type, book.Type);
            Assert.IsNotNull(book.Id);

            int bookCount = data.Storage.Stock.Count;
            data.Storage.Stock.Add(book);
            Assert.AreEqual(data.Storage.Stock.Count, bookCount + 1);
            List<IBook> books = new List<IBook>();
            books.Add(book);
            data.Storage.RemoveBooks(books);

        }

        [TestMethod]
        public void GetsTest()
        {
            IDataLayer data = PrepareData();
            IBook book = data.Storage.CreateBook("Hello", "World", "Author", 200, BookType.Horror);
            data.Storage.Stock.Add(book);
            List<Guid> bookIds = new List<Guid>();
            bookIds.Add(book.Id);
            foreach (var horror in data.Storage.GetBooksByTitle(BookType.Horror.ToString()))
            {
                Assert.AreEqual(horror.Type, BookType.Horror);
            }
            foreach (var author in data.Storage.GetBooksByAuthor("Author"))
            {
                Assert.AreEqual(author.Author, "Author");
            }
            foreach (var title in data.Storage.GetBooksByTitle("Hello"))
            {
                Assert.AreEqual(title.Title, "Hello");
            }
            foreach (var id in data.Storage.GetBooksById(bookIds))
            {
                bool trueId = false;
                foreach (var realId in bookIds)
                {
                    if (id.Id == realId)
                    { trueId = true; break; }

                }
                Assert.IsTrue(trueId);
            }
            foreach (var author in data.Storage.GetBooksByAuthor("Author"))
            {
                Assert.AreEqual(author.Author, "Author");
            }

        }
        [TestMethod]
        public void UpdateBooks()
        {
            IDataLayer data = PrepareData();
            IBook book1 = data.Storage.CreateBook("Hello", "World", "Author", 200, BookType.Horror);
            IBook book2 = data.Storage.CreateBook("Hi", "World", "Hania", 200, BookType.Romance);
            List<IBook> newBooks = new List<IBook>
            {
                book1,
                book2
            };
            Assert.AreEqual(0, data.Storage.Stock.Count);
            data.Storage.UpdateAllPrices(newBooks);
            Assert.AreEqual(2, data.Storage.Stock.Count);

            Assert.AreEqual(200, data.Storage.Stock[0].Price);
            Assert.AreEqual(200, data.Storage.Stock[1].Price);
            newBooks[0].Price = 100;
            data.Storage.UpdateAllPrices(newBooks);
            Assert.AreEqual(100, data.Storage.Stock[0].Price);
            Assert.AreEqual(200, data.Storage.Stock[1].Price);

            Assert.AreEqual(2, data.Storage.Stock.Count);
            newBooks.Remove(book1 as IBook);
            data.Storage.UpdateAllPrices(newBooks);
            Assert.AreEqual(1, data.Storage.Stock.Count);
            Assert.AreEqual(book2.Id, data.Storage.Stock[0].Id);
        }
    }
}