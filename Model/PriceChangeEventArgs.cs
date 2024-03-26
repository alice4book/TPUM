using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PriceChangeEventArgs : EventArgs
    {
        public PriceChangeEventArgs(Guid id, float price) 
        {
            Id = id;
            Price = price;
        }
        public Guid Id { get; }
        public float Price {  get; }
    }
}
