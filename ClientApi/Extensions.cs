using System;
using System.Collections.Generic;
using System.Text;

namespace ClientApi
{
    public static class Extensions
    {
        public static ArraySegment<byte> GetArraySegment(this string mesg)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(mesg);
            return new ArraySegment<byte>(buffer);
        }
    }
}
