using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectApi;

namespace ServerPresention
{
    class Program
    {
        private static IPresentationLayer _presentationLayer;

        private static WebSocketConnection _connection;
        static async Task Main(string[] args)
        {
            _presentationLayer = PresentationLayer.CreateDefault();
            await _presentationLayer.RunServer(8888);
        }
    }
}
