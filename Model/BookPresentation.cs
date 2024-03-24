using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BookPresentation
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }
        public string Type { get; set; }
        public Guid Id { get; set; }
        public BookPresentation(string title, string description, string author, float price, string type, Guid id)
        {
            Title = title;
            Description = description;
            Author = author;
            Price = price;
            Type = type;
            Id = id;
        }
    }
}
