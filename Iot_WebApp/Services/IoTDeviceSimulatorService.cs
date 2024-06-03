using System.Text;
using System.Text.Json;
using Iot_WebApp.Models;

namespace IoTWebApp.Services
{
    public class IoTDeviceSimulatorService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public IoTDeviceSimulatorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var client = new HttpClient();
            var random = new Random();

            while (!stoppingToken.IsCancellationRequested)
            {
                var temperature = random.Next(-10, 35);
                var humidity = random.Next(20, 80);

                var sensorData = new SensorData
                {
                    Temperature = temperature,
                    Humidity = humidity,
                    Timestamp = DateTime.Now
                };

                var content = new StringContent(JsonSerializer.Serialize(sensorData), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://localhost:7247/api/sensordata", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Data sent: {temperature}C, {humidity}%");
                }
                else
                {
                    Console.WriteLine("Failed to send data");
                }

                await Task.Delay(10000, stoppingToken); // Simulate every 10 seconds
            }
        }
    }
}
