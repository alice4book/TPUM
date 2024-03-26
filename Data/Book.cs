using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Book : IBook
    {
        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }
        public string Author { get; }
        public float Price
        { 
           get => price;
            set 
            {
                if (value == price)
                    return;

                price = value;
                
            }
        }
        private float price;
        public BookType Type { get; }

        public Book(string title, string description, string author, float price, BookType type)
        {
            Title = title;
            Description = description;
            Author = author;
            Price = price;
            Id = Guid.NewGuid();
            Type = type;
        }
    }
}
