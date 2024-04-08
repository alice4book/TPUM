using Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class LogicLayer : ILogicLayer
    {
        public override IShop Shop { get; }
        private IDataLayer Data { get; }
        public event Action<List<BookDTO>> onBookRemoved;
        public LogicLayer(IDataLayer data)
        {
            Data = data;
            Shop = new Shop(Data.Storage);
            data.Storage.onBookRemoved += HandleBookRemoved;
        }

        void HandleBookRemoved(List<IBook> books)
        {
            onBookRemoved?.Invoke(ToBookDTO(books));
        }
        internal static List<BookDTO> ToBookDTO(List<IBook> books)
        {
            List <BookDTO> result = new List <BookDTO>();
            foreach (var book in books)
            {
                result.Add(new BookDTO
                {
                     Title = book.Title,
                     Description = book.Description,
                     Author = book.Author,
                     Price = book.Price,
                     Type = book.Type.ToString(),
                     Id = book.Id
                });
            }
            return result;
        }
    }
}
