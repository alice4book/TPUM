﻿using Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Model
    {
        private ILogicLayer iLogicLayer;
        public string MainViewVisibility;
        public string CartViewVisibility;
        public CartPresentation CartPresentation;

        public event EventHandler<PriceChangeEventArgs>? PriceChanged;

        public StoragePresentation StoragePresentation { get; private set; }
        public Model(ILogicLayer? iLogicLayer) 
        {
            this.iLogicLayer = iLogicLayer == null ? ILogicLayer.Create() : iLogicLayer;
            StoragePresentation = new StoragePresentation(this.iLogicLayer.Shop);
            CartPresentation = new CartPresentation(new ObservableCollection<BookPresentation>(), this.iLogicLayer.Shop);
            MainViewVisibility = "Visiblie";
            CartViewVisibility = "Hidden";
            this.iLogicLayer.Shop.PriceChanged += OnPriceChanged;

        }

        public void OnPriceChanged(object sender, Logic.PriceChangeEventArgs e)
        {
            PriceChanged?.Invoke(this, new PriceChangeEventArgs(e.Id, e.Price));
        }
    }
}
