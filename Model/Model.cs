using Data;
using Logic;
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

        public event Action? Refresh;

        public StoragePresentation StoragePresentation { get; private set; }
        public Model(ILogicLayer? iLogicLayer) 
        {
            this.iLogicLayer = iLogicLayer == null ? ILogicLayer.Create() : iLogicLayer;
            StoragePresentation = new StoragePresentation(this.iLogicLayer.Shop);
            CartPresentation = new CartPresentation(new ObservableCollection<BookPresentation>(), this.iLogicLayer.Shop);
            MainViewVisibility = "Visible";
            CartViewVisibility = "Hidden";
        }

        public void RefreshBooks()
        {
            Refresh?.Invoke();
        }

        public async Task SellBooks()
        {
            await CartPresentation.Buy();
        }

        public async Task Connect()
        {
            await iLogicLayer.Connect(new Uri("ws://localhost:8888"));
        }

    }
}
