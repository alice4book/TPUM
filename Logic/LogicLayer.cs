using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class LogicLayer : ILogicLayer
    {
        public IShop Shop { get; private set;}
        private IDataLayer Data { get; }

        public LogicLayer(IDataLayer data)
        {
            Data = data;
            Shop = new Shop(Data.Storage);
        }
    }
}
