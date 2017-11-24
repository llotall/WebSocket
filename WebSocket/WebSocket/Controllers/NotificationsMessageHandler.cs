using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using WebSocketManager;

namespace WebSocket
{
    public class NotificationsMessageHandler : WebSocketHandler
    {
        public List<SocketUserOrder> socketUsers = new List<SocketUserOrder>();

        public NotificationsMessageHandler(WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {

        }

        public override async Task ReceiveAsync(System.Net.WebSockets.WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            
            var socketId = WebSocketConnectionManager.GetId(socket);
            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

            try
            {
                var recieveJSON = JsonConvert.DeserializeObject<SocketJSON>(message);
                if (recieveJSON.Type.Equals("order"))
                {
                    var socketUser = socketUsers.FirstOrDefault(u => u.OrderData == recieveJSON.Message);
                    if (socketUser != null)
                        socketUser.SocketID = socketId;
                    else
                        socketUsers.Add(new SocketUserOrder { SocketID = socketId, OrderData = recieveJSON.Message });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            var answer = JsonConvert.SerializeObject(new SocketJSON { Type = "return", Message = String.Empty });
            await SendMessageAsync(socketId, answer);
        }
    }

    public class SocketUserOrder
    {
        public string SocketID { get; set; }

        public string OrderData { get; set; }
    }

    public class SocketJSON
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("money")]
        public string Money { get; set; }
    }
}