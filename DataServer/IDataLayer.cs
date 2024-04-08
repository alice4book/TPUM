using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class IDataLayer
    {
        public static IDataLayer Create()
        {
            return new DataLayer();

        }
        public abstract IStorage Storage { get; set; }

        public event Action<string> onConnectionMessage;

    }
}
