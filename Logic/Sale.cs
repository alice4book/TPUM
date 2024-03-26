using Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Logic
{
    internal class Sale : ISale
    {
        private Guid BookId {  get; set; }
        private System.Timers.Timer DiscountTimer { get;}
        private float Discount { get; set; }
        private IStorage Storage { get; set; }
        private Random Rand { get; set; }

        public Sale(IStorage storage) 
        {
            DiscountTimer = new System.Timers.Timer(5000);
            DiscountTimer.Elapsed += GetNewSale;
            DiscountTimer.AutoReset = true;
            DiscountTimer.Enabled = true;
            Storage = storage;
            Rand = new Random();
        }
        public Tuple<Guid, float> GetSpecialOffer()
        {
            return new Tuple<Guid, float>(BookId, Discount);
        }
        private void GetNewSale(Object source, ElapsedEventArgs e)
        {
            Discount = ((float)Rand.NextDouble() * 0.5f) + 0.7f;
            IBook book = Storage.Stock[Rand.Next(0, Storage.Stock.Count)];
            BookId = book.Id;
            Storage.ChangePrice(BookId, book.Price * Discount);
        }
    }
}
