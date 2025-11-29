using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WWW.Data;
using WWW.Models;

namespace WWW.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Appointments
                .Include(a => a.Gym)
                .Include(a => a.Service)
                .Include(a => a.Trainer);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Gym)
                .Include(a => a.Service)
                .Include(a => a.Trainer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["GymId"] = new SelectList(_context.Gyms, "Id", "Name");
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "FullName");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GymId,MemberId,TrainerId,ServiceId,StartTime,EndTime,Price,Status")] Appointment appointment)

        {
            if (ModelState.IsValid)
            {
                appointment.CreatedAt = DateTime.UtcNow;
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GymId"] = new SelectList(_context.Gyms, "Id", "Name", appointment.GymId);
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "FullName", appointment.TrainerId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", appointment.ServiceId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["GymId"] = new SelectList(_context.Gyms, "Id", "Name", appointment.GymId);
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "FullName", appointment.TrainerId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", appointment.ServiceId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GymId,MemberId,TrainerId,ServiceId,StartTime,EndTime,Price,Status,CreatedAt")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GymId"] = new SelectList(_context.Gyms, "Id", "Name", appointment.GymId);
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "FullName", appointment.TrainerId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", appointment.ServiceId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Gym)
                .Include(a => a.Service)
                .Include(a => a.Trainer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }
    }
}
