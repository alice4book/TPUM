using Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class ILogicLayer
    {
        public abstract IShop Shop { get; }
        public static ILogicLayer Create(IDataLayer data = default(IDataLayer))
        {
            return new LogicLayer(data ?? IDataLayer.Create());
        }
        public abstract Task Connect(Uri uri);
    }
}
