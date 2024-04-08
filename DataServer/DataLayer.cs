using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataServer
{
    internal class DataLayer : IDataLayer
    {
        private IStorage storage;
     
        public override IStorage Storage { get => storage; set => storage = value; }

        internal DataLayer(IStorage storage = default) 
        {
            Storage = storage ?? new Storage();
        }
    }
}
