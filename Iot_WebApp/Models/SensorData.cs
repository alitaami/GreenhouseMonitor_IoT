namespace Iot_WebApp.Models
{
    public class SensorData
    {
        public int Id { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
