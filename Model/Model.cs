using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Model
    {
        private ILogicLayer iLogicLayer;

        public StoragePresentation storagePresentation { get; private set; }
        public Model(ILogicLayer? iLogicLayer) 
        {
            this.iLogicLayer = iLogicLayer == null ? ILogicLayer.Create() : iLogicLayer;
            storagePresentation = new StoragePresentation(iLogicLayer.Shop);
        }
    }
}
