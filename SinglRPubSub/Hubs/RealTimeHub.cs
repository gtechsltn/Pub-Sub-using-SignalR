using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SinglRPubSub.Hubs
{
    public class RealTimeHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task BroadcastData(string user ,object data)
        {
            await Clients.All.SendAsync("ReceiveData", user, data);
        }
    }
}
