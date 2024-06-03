using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Iot_WebApp.Models;

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
            var response = await _httpClient.GetAsync("https://localhost:9000/api/sensordata");
            var data = await response.Content.ReadAsStringAsync();
            var sensorData = JsonSerializer.Deserialize<List<SensorData>>(data);
            return View(sensorData);
        }
    }
}
