﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class PriceChangeEventArgs : EventArgs
    {
        public PriceChangeEventArgs(Guid id, float price) 
        { 
            Id = id;
            Price = price;
        }
        public PriceChangeEventArgs(PriceChangeEventArgs value)
        {
            Id = value.Id;
            Price = value.Price;
        }
        public Guid Id { get; }
        public float Price { get; set; }
    }

}
