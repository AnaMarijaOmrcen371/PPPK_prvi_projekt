using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalSystemAPI.Data;
using MedicalSystemAPI.Models;
using MedicalSystemAPI.DTOs;

namespace MedicalSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly MedicalContext _context;

        public AppointmentsController(MedicalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointments()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Select(a => new AppointmentDto
                {
                    Id = a.Id,
                    AppointmentDate = a.AppointmentDate,
                    ExaminationType = a.ExaminationType,
                    Notes = a.Notes,
                    PatientName = a.Patient!.FirstName + " " + a.Patient.LastName,
                    DoctorName = a.Doctor!.FirstName + " " + a.Doctor.LastName
                })
                .ToListAsync();

            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }

        [HttpPost]
        public async Task<ActionResult<Appointment>> CreateAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);

            await _context.SaveChangesAsync();

            return Ok(appointment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest();
            }

            _context.Entry(appointment).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<AppointmentReadDto>>> FilterAppointments(
    string? examinationType,
    int? patientId,
    int? doctorId,
    string? sortBy)
        {
            var query = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .AsQueryable();


            if (!string.IsNullOrEmpty(examinationType))
            {
                query = query.Where(a => a.ExaminationType == examinationType);
            }


            if (patientId.HasValue)
            {
                query = query.Where(a => a.PatientId == patientId);
            }


            if (doctorId.HasValue)
            {
                query = query.Where(a => a.DoctorId == doctorId);
            }


            if (sortBy == "date")
            {
                query = query.OrderBy(a => a.AppointmentDate);
            }

            if (sortBy == "patient")
            {
                query = query.OrderBy(a => a.Patient!.LastName);
            }

            if (sortBy == "doctor")
            {
                query = query.OrderBy(a => a.Doctor!.LastName);
            }

            var appointments = await query
                .Select(a => new AppointmentReadDto
                {
                    Id = a.Id,
                    AppointmentDate = a.AppointmentDate.ToString("dd.MM.yyyy HH:mm"),
                    Notes = a.Notes,
                    ExaminationType = a.ExaminationType,
                    PatientName = a.Patient!.FirstName + " " + a.Patient.LastName,
                    DoctorName = a.Doctor!.FirstName + " " + a.Doctor.LastName
                })
                .ToListAsync();

            return Ok(appointments);
        }
    }
}