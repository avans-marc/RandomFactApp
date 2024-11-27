using RandomFactApp.Domain.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace RandomFactApp.Infrastructure.SomeWebSocketClient
{
    public class SomeWebSocketClient : IWebSocketClient
    {
        // Should not be configured
        private const string WebsocketUri = "wss://echo.websocket.org";
        private readonly ClientWebSocket clientWebSocket;

        public SomeWebSocketClient()
        {
            clientWebSocket = new ClientWebSocket();
        }

        public async Task ConnectAsync()
        {
            await clientWebSocket.ConnectAsync(new Uri(WebsocketUri), CancellationToken.None);
        }

        public async Task<string> ReceiveDataAsync()
        {
            var receiveBuffer = new byte[1024];
            var receiveResult = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
            var receivedMessage = Encoding.UTF8.GetString(receiveBuffer, 0, receiveResult.Count);
            return receivedMessage; 
        }

        public async Task SendDataAsync(string message)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            await clientWebSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);

        }

        public async Task CloseAsync()
        {
            await clientWebSocket.CloseAsync( WebSocketCloseStatus.NormalClosure, null, CancellationToken.None );
        }
    }
}
