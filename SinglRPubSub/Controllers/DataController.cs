using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SinglRPubSub.Hubs;
using SinglRPubSub.Models;

namespace SinglRPubSub.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DataController : ControllerBase
    {
        private readonly IHubContext<RealTimeHub> _hubContext;

        public DataController(IHubContext<RealTimeHub> hubContext)
        {
            _hubContext = hubContext;
        }


        [HttpPost("send-message")]
        public async Task<IActionResult> ProcessMessage([FromBody] Message data)
        {
            // Process data here (e.g., save to DB, perform calculations)

            // Broadcast processed data to all clients
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", data.User, data.Content);

            return Ok(new { message = "Message processed and broadcast successfully." });
        }

        [HttpPost("send-data")]
        public async Task<IActionResult> ProcessData([FromBody] Data data)
        {
            // Process data here (e.g., save to DB, perform calculations)

            // Broadcast processed data to all clients
            await _hubContext.Clients.All.SendAsync("ReceiveData", data.User, data.Content);

            return Ok(new { message = "Data processed and broadcast successfully." });
        }
    }
}
