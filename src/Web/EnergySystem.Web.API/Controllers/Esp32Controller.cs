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
        private readonly HttpClient _httpClient;
        private readonly IHubContext<RelayHub> _hubContext;
        public Esp32Controller(HttpClient httpClient, IHubContext<RelayHub> hubContext)
        {
            this._httpClient = httpClient;
            this._hubContext = hubContext;
        }
        
        
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new { message = "ESP32 connected successfully!" });
        }

        [HttpGet("toggle")]
        public async Task<IActionResult> ToggleRelay()
        {
            string esp32Ip = "http://192.168.1.5";
            string requestUrl = $"{esp32Ip}/toggle-relay";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
                bool relayState = response.IsSuccessStatusCode;

                await this._hubContext.Clients.All.SendAsync("ReceiveRelayStatus", "ESP32_1", relayState);

                return Ok(new { message = "Relay toggled successfully", relayState });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = "Failed to communicate with ESP32", error = ex.Message });
            }
        }
    }
}
