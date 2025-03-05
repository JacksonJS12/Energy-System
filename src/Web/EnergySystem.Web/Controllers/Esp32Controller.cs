using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace EnergySystem.Web.Controllers
{
    using Microsoft.Extensions.Configuration;

    public class Esp32Controller : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public Esp32Controller(HttpClient httpClient, IConfiguration configuration)
        {
            this._httpClient = httpClient;
            this._apiBaseUrl = configuration.GetSection("ApiSettings")["BaseUrl"];
        }

        [HttpPost]
        public async Task<IActionResult> ToggleRelay()
        {
            var response = await this._httpClient.GetAsync($"{this._apiBaseUrl}/toggle");
            return Json(new { success = response.IsSuccessStatusCode });
        }
    }
}
