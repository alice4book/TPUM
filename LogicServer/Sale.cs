using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Logic
{
    internal class Sale : ISale
    {
        private Guid BookId {  get; set; }
        private float Discount { get; set; }

        private bool onSale = false;
        private IStorage Storage { get; set; }
        private Random Rand { get; set; }

        public Sale(IStorage storage) 
        {
            Storage = storage;
            Rand = new Random();
            GetNewSale();
        }
        public Tuple<Guid, float> GetSpecialOffer()
        {
            return new Tuple<Guid, float>(BookId, Discount);
        }
        private async void GetNewSale()
        {
            while (true) {
                IBook book;
                float waitSeconds = 5f;
                await Task.Delay((int)Math.Truncate(waitSeconds * 1000f));
                List<Guid> list = new List<Guid>();
                list.Add(BookId);
                if (onSale && Storage.GetBooksById(list).Count > 0)
                {
                    book = Storage.GetBooksById(list)[0];
                    book.Price = book.Price / Discount;
                    onSale = false;
                }
                if(Storage.Stock.Count > 0) {
                    Discount = ((float)Rand.NextDouble() * 0.5f) + 0.5f;
                    book = Storage.Stock[Rand.Next(0, Storage.Stock.Count)];
                    BookId = book.Id;
                    Storage.ChangePrice(BookId, book.Price * Discount);
                    onSale = true;  
                }
            }
        }
    }
}
