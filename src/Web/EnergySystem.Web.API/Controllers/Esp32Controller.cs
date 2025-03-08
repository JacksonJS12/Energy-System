using Microsoft.AspNetCore.Mvc;

namespace EnergySystem.Web.API.Controllers
{
    using System.Text.Json;

    using Hubs;

    using Microsoft.AspNetCore.SignalR;

    using Models;

    [ApiController]
    [Route("api/esp32")]
    public class Esp32Controller : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IHubContext<RelayHub> _relayHubContext;
        private readonly IHubContext<PowerPanelHub> _powerPanelHubContext;
        private static bool _batteryCharging = false; // Store current state
        public Esp32Controller(HttpClient httpClient, IHubContext<RelayHub> relayHubContext,
            IHubContext<PowerPanelHub> powerPanelHubContext)
        {
            this._httpClient = httpClient;
            this._relayHubContext = relayHubContext;
            this._powerPanelHubContext = powerPanelHubContext;
        }
        
        
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new { message = "ESP32 connected successfully!" });
        }

        [HttpGet("toggle")]
        public async Task<IActionResult> ToggleRelay()
        {
            string esp32Ip = "http://192.168.138.92";//"http://192.168.182.92"; 
            string requestUrl = $"{esp32Ip}/toggle-relay";

            try
            {
                HttpResponseMessage response = await this._httpClient.GetAsync(requestUrl);
                bool relayState = response.IsSuccessStatusCode;

                await this._relayHubContext.Clients.All.SendAsync("ReceiveRelayStatus", "ESP32_1", relayState);

                return Ok(new { message = "Relay toggled successfully", relayState });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = "Failed to communicate with ESP32", error = ex.Message });
            }
        }
        
        [HttpPost("button")]
        public async Task<IActionResult> ReceiveAcState()
        {
            using var reader = new StreamReader(this.Request.Body);
            string rawRequest = await reader.ReadToEndAsync();
            Console.WriteLine($"    Raw Request: {rawRequest}");

            var messageObject = JsonSerializer.Deserialize<AcMessageModel>(rawRequest);
            if (messageObject != null)
            {
                Console.WriteLine($"ESP32: {messageObject.Message}");

                // Broadcast AC state change to SignalR clients
                bool isAcOn = messageObject.Message.Contains("ON");
                await this._powerPanelHubContext.Clients.All.SendAsync("ReceiveAcState", isAcOn);

                return Ok(new { message = "Received successfully" });
            }

            return BadRequest("Invalid JSON format");
        }
        
        // [HttpPost("voltage")]
        // public async Task<IActionResult> ReceiveVoltage()
        // {
        //     using var reader = new StreamReader(Request.Body);
        //     string rawRequest = await reader.ReadToEndAsync();
        //     Console.WriteLine($"Voltage Received: {rawRequest}");
        //
        //     var voltageObject = JsonSerializer.Deserialize<VoltageModel>(rawRequest);
        //     if (voltageObject != null)
        //     {
        //         Console.WriteLine($"Voltage: {voltageObject.Voltage} V");
        //         return Ok(new { message = "Voltage received successfully" });
        //     }
        //
        //     return BadRequest("Invalid JSON format");
        // }
        
        [HttpGet("charge-battery")]
        public async Task<IActionResult> ChargeBattery()
        {
            string esp32Ip = "http://192.168.138.92";//"http://192.168.1.5";//"http://192.168.182.92"; 
            string requestUrl = $"{esp32Ip}/charge-battery";

            try
            {
                HttpResponseMessage response = await this._httpClient.GetAsync(requestUrl);
                bool relayState = response.IsSuccessStatusCode;

                await this._relayHubContext.Clients.All.SendAsync("ReceiveRelayStatus", "ESP32_1", relayState);

                return Ok(new { message = "Relay toggled successfully", relayState });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = "Failed to communicate with ESP32", error = ex.Message });
            }
        }
    }
}
