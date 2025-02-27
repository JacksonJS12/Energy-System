using Microsoft.AspNetCore.Mvc;

namespace EnergySystem.Web.API.Controllers
{
    using Hubs;

    using Microsoft.AspNetCore.SignalR;

    using Models;

    [ApiController]
    [Route("api/esp32")]
    public class Esp32Controller : ControllerBase
    {
        private readonly IHubContext<DataHub> _hubContext;
        
        
        public Esp32Controller(IHubContext<DataHub> hubContext)
        {
            this._hubContext = hubContext;
        }
        
        
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new { message = "ESP32 connected successfully!" });
        }
        
        [HttpPost("send-data")]
        public async Task<IActionResult> ReceiveData([FromBody] Esp32DataModel data)
        {
            // Log data (optional)
            Console.WriteLine($"Received from ESP32: {data.DeviceId} - Voltage: {data.Voltage}V, Current: {data.Current}A");

            // Send data to SignalR clients (MVC app)
            await this._hubContext.Clients.All.SendAsync("ReceiveData", data.DeviceId, data.Voltage, data.Current);

            return Ok(new { message = "Data received successfully!" });
        }
    }
}
