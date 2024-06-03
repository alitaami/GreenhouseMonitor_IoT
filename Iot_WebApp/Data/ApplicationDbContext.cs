using Iot_WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Iot_WebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<SensorData> SensorData { get; set; }
    }
}
