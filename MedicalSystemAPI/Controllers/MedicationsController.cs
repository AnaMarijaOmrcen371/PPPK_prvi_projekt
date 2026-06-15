using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalSystemAPI.Data;
using MedicalSystemAPI.Models;
using MedicalSystemAPI.DTOs;

namespace MedicalSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationsController : ControllerBase
    {
        private readonly MedicalContext _context;

        public MedicationsController(MedicalContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicationDto>>> GetMedications()
        {
            return await _context.Medications
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .Select(m => new MedicationDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Dose = m.Dose,
                    Frequency = m.Frequency,
                    PatientName = m.Patient!.FirstName + " " + m.Patient.LastName,
                    DoctorName = m.Doctor!.FirstName + " " + m.Doctor.LastName
                })
                .ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicationDto>> GetMedication(int id)
        {
            var medication = await _context.Medications
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medication == null)
            {
                return NotFound();
            }

            var medicationDto = new MedicationDto
            {
                Id = medication.Id,
                Name = medication.Name,
                Dose = medication.Dose,
                Frequency = medication.Frequency,
                PatientName = medication.Patient!.FirstName + " " + medication.Patient.LastName,
                DoctorName = medication.Doctor!.FirstName + " " + medication.Doctor.LastName
            };

            return medicationDto;
        }

       
        [HttpPost]
        public async Task<ActionResult<Medication>> CreateMedication(Medication medication)
        {
            _context.Medications.Add(medication);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetMedication),
                new { id = medication.Id },
                medication
            );
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedication(int id, Medication medication)
        {
            if (id != medication.Id)
            {
                return BadRequest();
            }

            _context.Entry(medication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Medications.Any(e => e.Id == id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedication(int id)
        {
            var medication = await _context.Medications.FindAsync(id);

            if (medication == null)
            {
                return NotFound();
            }

            _context.Medications.Remove(medication);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<MedicationReadDto>>> FilterMedications(
    string? medicationName,
    int? patientId,
    string? dosage,
    string? sortBy)
        {
            var query = _context.Medications
                .Include(m => m.Patient)
                .AsQueryable();


            if (!string.IsNullOrEmpty(medicationName))
            {
                query = query.Where(m => m.Name.Contains(medicationName));
            }


            if (patientId.HasValue)
            {
                query = query.Where(m => m.PatientId == patientId);
            }


            if (!string.IsNullOrEmpty(dosage))
            {
                query = query.Where(m => m.Dose.Contains(dosage));
            }


            if (sortBy == "name")
            {
                query = query.OrderBy(m => m.Name);
            }

            if (sortBy == "patient")
            {
                query = query.OrderBy(m => m.Patient!.LastName);
            }

            var medications = await query
                .Select(m => new MedicationReadDto
                {
                    Id = m.Id,
                    MedicationName = m.Name,
                    Dosage = m.Dose,
                    Frequency = m.Frequency,
                    PatientName = m.Patient!.FirstName + " " + m.Patient.LastName
                })
                .ToListAsync();

            return Ok(medications);
        }
    }
}