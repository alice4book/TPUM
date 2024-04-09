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

        public event EventHandler<PriceChangeEventArgs>? PriceChanged;
        public event Action? Refresh;

        public StoragePresentation StoragePresentation { get; private set; }
        public Model(ILogicLayer? iLogicLayer) 
        {
            this.iLogicLayer = iLogicLayer == null ? ILogicLayer.Create() : iLogicLayer;
            StoragePresentation = new StoragePresentation(this.iLogicLayer.Shop);
            CartPresentation = new CartPresentation(new ObservableCollection<BookPresentation>(), this.iLogicLayer.Shop);
            MainViewVisibility = "Visible";
            CartViewVisibility = "Hidden";
            this.iLogicLayer.Shop.PriceChanged += OnPriceChanged;
            this.iLogicLayer.onBookRemoved += HandleBookRemoved;
            this.iLogicLayer.onBookAdded += HandleBookAdded;

        }

        public void OnPriceChanged(object sender, Logic.PriceChangeEventArgs e)
        {
            PriceChanged?.Invoke(this, new PriceChangeEventArgs(e.Id, e.Price));
        }

        public void HandleBookRemoved(List<BookDTO> books)
        {
            Refresh?.Invoke();
        }

        public void Connect()
        {
            iLogicLayer.Connect(new Uri("ws://localhost:8888"));
        }

        public void HandleBookAdded(BookDTO info)
        {
            Refresh?.Invoke();
        }
    }
}
