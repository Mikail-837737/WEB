using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WWW.Data;
using WWW.Models;

namespace WWW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrainersApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/trainersapi
        // Tüm antrenörleri JSON olarak listeler
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerDto>>> GetAll()
        {
            var trainers = await _context.Trainers
                .Select(t => new TrainerDto
                {
                    Id = t.Id,
                    FullName = t.FullName,
                    Specializations = t.Specializations,
                    GymName = t.Gym.Name
                })
                .ToListAsync();

            return Ok(trainers);
        }

        // GET: api/trainersapi/available?date=2025-11-29T15:00:00&gymId=1
        // Belirli tarihte / saatte uygun antrenörleri getirir
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<TrainerDto>>> GetAvailable(
            DateTime date,int gymId)
        {
            var day = date.DayOfWeek;
            var time = TimeOnly.FromDateTime(date);

            var trainers = await _context.Trainers
                .Where(t => t.GymId == gymId)
                .Where(t => _context.TrainerAvailabilities.Any(av =>
                    av.TrainerId == t.Id &&
                    av.Day == day &&
                    av.StartTime <= time &&
                    time <= av.EndTime))
                .Select(t => new TrainerDto
                {
                    Id = t.Id,
                    FullName = t.FullName,
                    Specializations = t.Specializations,
                    GymName = t.Gym.Name
                })
                .ToListAsync();

            return Ok(trainers);
        }
    }
}
