using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Iot_WebApp.Data;
using Iot_WebApp.Models;

namespace IoTWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorDataController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SensorDataController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostSensorData([FromBody] SensorData sensorData)
        {
            if (ModelState.IsValid)
            {
                _context.SensorData.Add(sensorData);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetSensorData()
        {
            var data = await _context
                                    .SensorData
                                    .OrderByDescending(s=>s.Id)
                                    .ToListAsync();
            return Ok(data);
        }
    }
}
