namespace EnergySystem.Web.Hubs
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;

    public class PowerPanelHub : Hub
    {
        public async Task UpdateEnergyUsage(double energyUsage)
        {
            await Clients.All.SendAsync("UpdateEnergyUsage", energyUsage);
        }

        public async Task ReceiveAcState(bool isAcOn)
        {
            await Clients.All.SendAsync("ReceiveAcState", isAcOn);
        }
    }
}
