using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public interface ISale
    {
        Tuple<Guid, float> GetSpecialOffer();
    }
}
