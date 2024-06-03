using System.Text;
using System.Text.Json;
using Iot_WebApp.Common;
using Iot_WebApp.Common.Utilities;
using Iot_WebApp.Models;
using Microsoft.Extensions.DependencyModel;

namespace IoTWebApp.Services
{
    public class IotDeviceSimulatorService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public IotDeviceSimulatorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var client = new HttpClient();
            var random = new Random();

            while (!stoppingToken.IsCancellationRequested)
            {
                var temperature = random.Next(20, 40);
                var humidity = random.Next(25, 80);

                if (temperature > 35)
                {
                    // Compose the email body in Persian
                    string subject = Resource.EmailSubject;
                    string body = Resource.EmailBody;

                    // Send the email
                    try
                    {
                        await SendMail.SendAsync("alitaami81@gmail.com", subject, body);
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions
                        Console.WriteLine("An error occurred while sending the email: " + ex.Message);
                    }
                }

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

                await Task.Delay(5000, stoppingToken); // Simulate every 5 seconds
            }
        }
    }
}
