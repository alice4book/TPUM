﻿using DataServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicServer
{
    public abstract class ILogicLayer
    {
        public abstract IShop Shop { get; }
        public event Action<List<BookDTO>> onBookRemoved;
        public event Action<string> onConnectionMessage;
        public static ILogicLayer Create(IDataLayer data = default(IDataLayer))
        {
            return new LogicLayer(data ?? IDataLayer.Create());
        }
        public static ILogicLayer CreateDefault()
        {
            return new LogicLayer(IDataLayer.Create());
        }
    }
}
