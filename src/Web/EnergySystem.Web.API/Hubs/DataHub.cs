namespace EnergySystem.Web.API.Hubs
{
    using Microsoft.AspNetCore.SignalR;

    public class DataHub : Hub
    {
        public async Task SendData(string esp32Id, float voltage, float current)
        {
            await this.Clients.All.SendAsync("ReceiveData", esp32Id, voltage, current);
        }
    }
}
