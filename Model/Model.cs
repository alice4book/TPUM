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

        public StoragePresentation StoragePresentation { get; private set; }
        public Model(ILogicLayer? iLogicLayer) 
        {
            this.iLogicLayer = iLogicLayer == null ? ILogicLayer.Create() : iLogicLayer;
            StoragePresentation = new StoragePresentation(this.iLogicLayer.Shop);
            CartPresentation = new CartPresentation(new ObservableCollection<BookPresentation>(), this.iLogicLayer.Shop);
            MainViewVisibility = "Visiblie";
            CartViewVisibility = "Hidden";

        }

    }
}
