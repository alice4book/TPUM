using Data;
using Logic;

namespace LogicTest
{
    [TestClass]
    public class LogicTest
    {
        private ILogicLayer logicLayer = ILogicLayer.Create(new DataMock());

        [TestMethod]
        public void SellBook()
        {
            BookDTO book = logicLayer.Shop.GetBooks()[0];
            List<BookDTO> bookDTOs = new List<BookDTO>(); 
            bookDTOs.Add(book);
            int bookCount = logicLayer.Shop.GetBooks().Count;
            logicLayer.Shop.Sell(bookDTOs);
            Assert.AreEqual(bookCount - 1, logicLayer.Shop.GetBooks().Count);
        }

        [TestMethod]
        public void GetBooks()
        {
            int count = logicLayer.Shop.GetBooks().Count;
            Assert.AreEqual(count, 2);
        }

    }
}