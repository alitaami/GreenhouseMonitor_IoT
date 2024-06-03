using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Iot_WebApp.Models;
using Newtonsoft.Json;

namespace IoTWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7247/api/sensordata");
            var data = await response.Content.ReadAsStringAsync();
            var sensorData = JsonConvert.DeserializeObject<List<SensorData>>(data);
            return View(sensorData);
        }
    }
}
