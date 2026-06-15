using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalSystemAPI.Data;
using MedicalSystemAPI.Models;
using MedicalSystemAPI.DTOs;

namespace MedicalSystemAPI.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly MedicalContext _context;

        public DoctorsController(MedicalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctors()
        {
            var doctors = await _context.Doctors
                .Select(d => new DoctorDto
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    Specialization = d.Specialization
                })
                .ToListAsync();

            return Ok(doctors);
        }

        [HttpPost]
        public async Task<ActionResult<Doctor>> CreateDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);

            await _context.SaveChangesAsync();

            return Ok(doctor);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return BadRequest();
            }

            _context.Entry(doctor).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<DoctorReadDto>>> FilterDoctors(
    string? firstName,
    string? lastName,
    string? specialization,
    string? sortBy)
        {
            var query = _context.Doctors.AsQueryable();


            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(d => d.FirstName.Contains(firstName));
            }


            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(d => d.LastName.Contains(lastName));
            }


            if (!string.IsNullOrEmpty(specialization))
            {
                query = query.Where(d => d.Specialization.Contains(specialization));
            }


            if (sortBy == "firstname")
            {
                query = query.OrderBy(d => d.FirstName);
            }

            if (sortBy == "lastname")
            {
                query = query.OrderBy(d => d.LastName);
            }

            if (sortBy == "specialization")
            {
                query = query.OrderBy(d => d.Specialization);
            }

            var doctors = await query
                .Select(d => new DoctorReadDto
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    Specialization = d.Specialization
                })
                .ToListAsync();

            return Ok(doctors);
        }
    }
}