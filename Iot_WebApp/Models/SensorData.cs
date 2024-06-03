namespace Iot_WebApp.Models
{
    public class SensorData
    {
        public int Id { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public DateTime Timestamp { get; set; }  
    }
}
