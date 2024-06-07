using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace IoTWebApp.Hubs
{
    public class SensorDataHub : Hub
    {
        public async Task SendSensorData(object sensorData)
        {
            await Clients.All.SendAsync("ReceiveSensorData", sensorData);
        }
    }
}
