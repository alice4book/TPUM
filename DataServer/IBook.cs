using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IBook
    {
        string Title { get; }
        string Description { get; }
        string Author { get; }
        float Price { get; set; }
        BookType Type { get; }
        Guid Id { get; }
    }
}