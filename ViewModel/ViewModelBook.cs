using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ViewModelBook
    {
        public ViewModelBook(string title, string description, string author, float price, string type, Guid id)
        {
            Title = title;
            Description = description;
            Author = author;
            Price = price;
            Type = type;
            Id = id;
        }

        public ViewModelBook(BookPresentation book)
        {
            Title = book.Title;
            Description = book.Description;
            Author = book.Author;
            Price = book.Price;
            Type = book.Type;
            Id = book.Id;
        }

        public enum BookType
        {
            Horror,
            Comedy,
            Criminal,
            Romance,
            Fantasy,
            Adventure
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }
        public string Type { get; set; }
        public Guid Id { get; set; }

    }
}
