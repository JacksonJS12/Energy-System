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
        public Esp32Controller(HttpClient httpClient)
        {
            this._httpClient = httpClient;
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
                HttpResponseMessage response = await this._httpClient.GetAsync(requestUrl);
                string responseBody = await response.Content.ReadAsStringAsync();

                return Ok(new { message = "Relay toggled successfully", espResponse = responseBody });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = "Failed to communicate with ESP32", error = ex.Message });
            }
        }
    }
}
