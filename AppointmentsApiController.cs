using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WWW.Data;
using WWW.Models;

namespace WWW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/appointmentsapi/by-member?memberId=103
        // Üyenin tüm randevularını getirir
        [HttpGet("by-member")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetByMember(string memberId)
        {
            var items = await _context.Appointments
                .Include(a => a.Gym)
                .Include(a => a.Trainer)
                .Include(a => a.Service)
                .Where(a => a.MemberId == memberId)
                .OrderByDescending(a => a.StartTime)
                .ToListAsync();

            return items;
        }
    }
}
