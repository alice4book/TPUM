using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IDataLayer
    {
        public IStorage Storage { get; set; }

        public static IDataLayer Create(IStorage storage = default)
        {
            return new DataLayer(storage);
        }
    }
}
