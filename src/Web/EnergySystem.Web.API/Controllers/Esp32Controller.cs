using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using EnergySystem.Web.API.Hubs;
using EnergySystem.Web.API.Models;
using System.Text.Json;

namespace EnergySystem.Web.API.Controllers;

[ApiController]
[Route("api/esp32")]
public class Esp32Controller : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IHubContext<RelayHub> _relayHubContext;
    private readonly IHubContext<PowerPanelHub> _powerPanelHubContext;

    private const string Esp32Address = "http://192.168.190.229";

    private static bool _isGridPower = true;
    private static bool _batteryCharging = false;

    public Esp32Controller(HttpClient httpClient, 
                           IHubContext<RelayHub> relayHubContext,
                           IHubContext<PowerPanelHub> powerPanelHubContext)
    {
        this._httpClient = httpClient;
        this._relayHubContext = relayHubContext;
        this._powerPanelHubContext = powerPanelHubContext;
    }

    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok(new { message = "ESP32 API is reachable." });
    }

    // Switch between Grid Power and Battery Power
    [HttpGet("toggle")]
    public async Task<IActionResult> ToggleRelay()
    {
        string requestUrl = $"{Esp32Address}/toggle-relay";

        try
        {
            HttpResponseMessage response = await this._httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                _isGridPower = !_isGridPower;
                string relayStatus = _isGridPower ? "Grid Power ON" : "Battery Power ON";

                await this._relayHubContext.Clients.All.SendAsync("ReceiveRelayStatus", "ESP32_1", relayStatus);

                return Ok(new { message = relayStatus });
            }

            return StatusCode((int)response.StatusCode, "ESP32 response error.");
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, new { message = "Failed to communicate with ESP32.", error = ex.Message });
        }
    }

    // Toggle Battery Charging ON/OFF
    [HttpGet("charge-battery")]
    public async Task<IActionResult> ChargeBattery()
    {
        string requestUrl = $"{Esp32Address}/charge-battery";

        try
        {
            HttpResponseMessage response = await this._httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                _batteryCharging = !_batteryCharging;
                string chargeStatus = _batteryCharging ? "Battery Charging ON" : "Battery Charging OFF";

                await this._relayHubContext.Clients.All.SendAsync("ReceiveRelayStatus", "ESP32_1", chargeStatus);

                return Ok(new { message = chargeStatus });
            }

            return StatusCode((int)response.StatusCode, "ESP32 response error.");
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, new { message = "Failed to communicate with ESP32.", error = ex.Message });
        }
    }

    // Receives AC state from ESP32 button press
    [HttpPost("button")]
    public async Task<IActionResult> ReceiveAcState()
    {
        using var reader = new StreamReader(Request.Body);
        string rawRequest = await reader.ReadToEndAsync();

        var messageObject = JsonSerializer.Deserialize<AcMessageModel>(rawRequest);
        if (messageObject != null)
        {
            bool isAcOn = messageObject.Message.Contains("ON");
            await this._powerPanelHubContext.Clients.All.SendAsync("ReceiveAcState", isAcOn);

            Console.WriteLine($"ESP32 AC State: {(isAcOn ? "ON" : "OFF")}");

            return Ok(new { message = "AC state received successfully" });
        }

        return BadRequest("Invalid JSON format");
    }
    
    [HttpPost("voltage")]
    public async Task<IActionResult> ReceiveVoltage([FromBody] VoltageModel voltage)
    {
        Console.WriteLine($"Battery Voltage: {voltage.Voltage} V");

        // Optional: send voltage via SignalR to frontend
        await _powerPanelHubContext.Clients.All.SendAsync("ReceiveVoltage", voltage.Voltage);

        return Ok(new { status = "success", voltage = voltage.Voltage });
    }
}
