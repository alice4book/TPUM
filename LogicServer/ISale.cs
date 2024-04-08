using System;
using System.Collections.Generic;
using System.Text;

namespace LogicServer
{
    public interface ISale
    {
        Tuple<Guid, float> GetSpecialOffer();
    }
}
