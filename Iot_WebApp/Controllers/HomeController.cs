using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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

        public async Task<IActionResult> Index(int? page, int pageSize = 10, decimal? minTemperature = null, decimal? maxTemperature = null, decimal? minHumidity = null, decimal? maxHumidity = null)
        {
            int pageNumber = page ?? 1;

            var response = await _httpClient.GetAsync("https://localhost:7247/api/sensordata");
            var data = await response.Content.ReadAsStringAsync();
            var sensorData = JsonConvert.DeserializeObject<List<SensorData>>(data);

            // Filter sensor data based on provided temperature and humidity ranges
            if (minTemperature.HasValue)
            {
                sensorData = sensorData.Where(s => s.Temperature >= minTemperature.Value).ToList();
            }

            if (maxTemperature.HasValue)
            {
                sensorData = sensorData.Where(s => s.Temperature <= maxTemperature.Value).ToList();
            }

            if (minHumidity.HasValue)
            {
                sensorData = sensorData.Where(s => s.Humidity >= minHumidity.Value).ToList();
            }

            if (maxHumidity.HasValue)
            {
                sensorData = sensorData.Where(s => s.Humidity <= maxHumidity.Value).ToList();
            }

            var paginatedData = sensorData.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = sensorData.Count;

            return View(paginatedData);
        }
    }
}