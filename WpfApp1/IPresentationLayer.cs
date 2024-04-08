using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPresention
{
    public interface IPresentationLayer
    {
        Task RunServer(int port);
        void ConnectionMessageHandler(string message);
        Task SendMessage(string message);
    }
}
