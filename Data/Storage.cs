﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Storage : IStorage
    {
        public event EventHandler<PriceChangeEventArgs> PriceChange;
        public List<IBook> Stock { get; }
        public Storage()
        {
            Stock = new List<IBook>();

            Stock.Add(new Book("It", "Clown scary!", "Stephen King", 59.99f, BookType.Horror));
            Stock.Add(new Book("This", "Clown scary!", "Stephen King", 19.99f, BookType.Romance));
            Stock.Add(new Book("That", "Clown scary!", "Stephen King", 29.99f, BookType.Horror));
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

        public void ChangePrice(Guid id, float newPrice)
        {
            IBook book = Stock.Find(x => x.Id.Equals(id));
            if (book == null)
                return;
            if(Math.Abs(newPrice = book.Price) < 0.01f)
                return;
            book.Price = newPrice;
            OnPriceChanged(book.Id, book.Price);
        }

        private void OnPriceChanged(Guid id, float price)
        {
            EventHandler<PriceChangeEventArgs> handler = PriceChange;
            handler?.Invoke(this, new PriceChangeEventArgs(id, price));
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

    }
}
