using Microsoft.AspNetCore.SignalR;

namespace EnergySystem.Web.API.Hubs
{
    public class RelayHub : Hub
    {
        public async Task SendRelayStatus(string esp32Id, bool state)
        {
            await Clients.All.SendAsync("ReceiveRelayStatus", esp32Id, state);
        }
    }
}
