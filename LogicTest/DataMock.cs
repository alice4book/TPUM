﻿using Data;
using System.Diagnostics;
using static System.Reflection.Metadata.BlobBuilder;

namespace LogicTest
{
    public class DataMock : IDataLayer
    {
        private readonly IStorage storage = new StorageMock();
        public override IStorage Storage
        {
            get { return storage; }
        }

       
    }

    public class BookMock : IBook
    {
        public string Title { get; }

        public string Description { get; }

        public string Author { get; }

        private float price;
        public float Price {
            get => price;
            set
            {
                if (value == price)
                    return;

                price = value;

            }
        }

        public BookType Type { get; }

        public Guid Id { get; }
    }

    public class StorageMock : IStorage
    {
        public List<IBook> Stock { get; }

        public event EventHandler<PriceChangeEventArgs> PriceChange;

        public StorageMock()
        {
            Stock = new List<IBook>
            {
                new Book("It", "Clown scary!", "Stephen King", 59.99f, BookType.Horror),
                new Book("This", "Clown scary!", "Stephen King", 19.99f, BookType.Romance)
            };
        }

        public void ChangePrice(Guid id, float newPrice)
        {
            foreach (var book in Stock) 
            {
                if (book.Id == id)
                {
                    book.Price = newPrice;
                    break;
                }
            }
        }

        public IBook CreateBook(string title, string description, string author, float price, BookType type)
        {
            return new Book(title, description, author, price, type);
        }

        public List<IBook> GetBooksByAuthor(string author)
        {
            return Stock.FindAll(book => book.Author == author);
        }

        public List<IBook> GetBooksById(List<Guid> Ids)
        {
            List<IBook> books = new List<IBook>();
            foreach (Guid id in Ids)
            {
                List<IBook> tmp = Stock.FindAll(x => x.Id == id);
                if (tmp.Count > 0)
                    books.AddRange(tmp);
            }
            return books;
        }

        public List<IBook> GetBooksByTitle(string title)
        {
            return Stock.FindAll(book => book.Title == title);
        }

        public List<IBook> GetBooksOfType(BookType type)
        {
            return Stock.FindAll(book => book.Type == type);
        }

        public void RemoveBooks(List<IBook> books)
        {
            books.ForEach(book => Stock.Remove(book));
        }
    }
}
