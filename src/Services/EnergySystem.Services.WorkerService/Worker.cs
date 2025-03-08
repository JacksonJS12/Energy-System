using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EnergySystem.Services.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly HttpClient _httpClient;
        private readonly HubConnection _hubConnection;
        private bool _isAcOn = false;
        private double _energyUsage = 10.00; // Initial kW/h
        private double _increment = 0.05;

        public Worker(ILogger<Worker> logger)
        {
            this._logger = logger;
            this._httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            });

            this._hubConnection = new HubConnectionBuilder()
                .WithUrl("https://192.168.138.92:5001/powerpanelhub", options =>
                {
                    options.HttpMessageHandlerFactory = _ => new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                    };
                })
                .WithAutomaticReconnect()
                .Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await this._hubConnection.StartAsync();
            this._logger.LogInformation("Worker Service Started");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    bool newAcState = await CheckAcState();
                    if (newAcState != this._isAcOn)
                    {
                        this._isAcOn = newAcState;
                        this._logger.LogInformation($"AC State Changed: {(this._isAcOn ? "ON" : "OFF")}");
                    }

                    if (this._isAcOn)
                    {
                        UpdateEnergyUsage();
                        await this._hubConnection.SendAsync("UpdateEnergyUsage", this._energyUsage, stoppingToken);
                        this._logger.LogInformation($"Energy Usage Updated: {this._energyUsage} kW/h");
                    }

                    await Task.Delay(5000, stoppingToken);  // Poll every 5s
                }
                catch (Exception ex)
                {
                    this._logger.LogError($"Worker Service Error: {ex.Message}");
                }
            }
        }

        private async Task<bool> CheckAcState()
        {
            try
            {
                HttpResponseMessage response = await this._httpClient.GetAsync("https://192.168.138.92:5001/api/esp32/state");
                if (!response.IsSuccessStatusCode) return this._isAcOn;

                var json = await response.Content.ReadAsStringAsync();
                var state = JsonSerializer.Deserialize<AcStateModel>(json);
                return state?.IsAcOn ?? this._isAcOn;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"API Call Failed: {ex.Message}");
                return this._isAcOn;
            }
        }

        private void UpdateEnergyUsage()
        {
            this._energyUsage += this._increment;
            if (this._increment == 0.05) this._increment = 0.03;
            else if (this._increment == 0.03) this._increment = 0.01;
        }
    }

    public class AcStateModel
    {
        public bool IsAcOn { get; set; }
    }
}
