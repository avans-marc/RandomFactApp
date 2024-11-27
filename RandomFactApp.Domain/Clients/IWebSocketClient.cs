using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomFactApp.Domain.Clients
{
    public interface IWebSocketClient
    {
        Task ConnectAsync();
        Task<string> ReceiveDataAsync();
        Task SendDataAsync(string data);
        Task CloseAsync();
    }
}
