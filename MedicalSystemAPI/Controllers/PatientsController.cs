using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalSystemAPI.Data;
using MedicalSystemAPI.Models;
using MedicalSystemAPI.DTOs;

namespace MedicalSystemAPI.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly MedicalContext _context;

        public PatientsController(MedicalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetPatients()
        {
            var patients = await _context.Patients
                .Select(p => new PatientDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Oib = p.OIB,
                    DateOfBirth = p.DateOfBirth,
                    Gender = p.Gender,
                    ResidenceAddress = p.ResidenceAddress,
                    LivingAddress = p.LivingAddress
                })
                .ToListAsync();

            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
        {
            _context.Patients.Add(patient);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPatient),
                new { id = patient.Id },
                patient
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<PatientReadDto>>> FilterPatients(
     string? gender,
     string? city,
     string? firstName,
     string? lastName,
     string? sortBy)
        {
            var query = _context.Patients.AsQueryable();


            if (!string.IsNullOrEmpty(gender))
            {
                query = query.Where(p => p.Gender == gender);
            }


            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(p => p.ResidenceAddress.Contains(city));
            }


            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(p => p.FirstName == firstName);
            }


            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(p => p.LastName == lastName);
            }


            if (sortBy == "lastname")
            {
                query = query.OrderBy(p => p.LastName);
            }

            if (sortBy == "firstname")
            {
                query = query.OrderBy(p => p.FirstName);
            }

            var patients = await query
                .Select(p => new PatientReadDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    OIB = p.OIB,
                    Gender = p.Gender,
                    ResidenceAddress = p.ResidenceAddress
                })
                .ToListAsync();

            return Ok(patients);
        }
    }
}