using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class DataLayer : IDataLayer
    {
        internal DataLayer(IStorage storage = default) 
        {
            Storage = storage ?? new Storage(); 
        }                        
        public override IStorage Storage { get; set;}
    }
}
