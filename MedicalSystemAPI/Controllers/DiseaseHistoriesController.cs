using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalSystemAPI.Data;
using MedicalSystemAPI.Models;
using MedicalSystemAPI.DTOs;

namespace MedicalSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiseaseHistoriesController : ControllerBase
    {
        private readonly MedicalContext _context;

        public DiseaseHistoriesController(MedicalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiseaseHistoryDto>>> GetDiseaseHistories()
        {
            var diseaseHistories = await _context.DiseaseHistories
                .Include(d => d.Patient)
                .Include(d => d.Doctor)
                .Select(d => new DiseaseHistoryDto
                {
                    Id = d.Id,
                    Diagnosis = d.Diagnosis,
                    DateRecorded = d.DateRecorded,
                    PatientName = d.Patient!.FirstName + " " + d.Patient.LastName,
                    DoctorName = d.Doctor!.FirstName + " " + d.Doctor.LastName
                })
                .ToListAsync();

            return Ok(diseaseHistories);
        }

        [HttpPost]
        public async Task<ActionResult<DiseaseHistory>> CreateDiseaseHistory(DiseaseHistory diseaseHistory)
        {
            _context.DiseaseHistories.Add(diseaseHistory);

            await _context.SaveChangesAsync();

            return Ok(diseaseHistory);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<DiseaseHistoryReadDto>>> FilterDiseaseHistories(
    string? diagnosis,
    int? patientId,
    int? doctorId,
    string? sortBy)
        {
            var query = _context.DiseaseHistories
                .Include(d => d.Patient)
                .Include(d => d.Doctor)
                .AsQueryable();


            if (!string.IsNullOrEmpty(diagnosis))
            {
                query = query.Where(d => d.Diagnosis.Contains(diagnosis));
            }


            if (patientId.HasValue)
            {
                query = query.Where(d => d.PatientId == patientId);
            }


            if (doctorId.HasValue)
            {
                query = query.Where(d => d.DoctorId == doctorId);
            }


            if (sortBy == "date")
            {
                query = query.OrderBy(d => d.DateRecorded);
            }

            if (sortBy == "diagnosis")
            {
                query = query.OrderBy(d => d.Diagnosis);
            }

            if (sortBy == "patient")
            {
                query = query.OrderBy(d => d.Patient!.LastName);
            }

            var diseaseHistories = await query
                .Select(d => new DiseaseHistoryReadDto
                {
                    Id = d.Id,
                    Diagnosis = d.Diagnosis,
                    DateRecorded = d.DateRecorded.ToString("dd.MM.yyyy"),
                    PatientName = d.Patient!.FirstName + " " + d.Patient.LastName,
                    DoctorName = d.Doctor!.FirstName + " " + d.Doctor.LastName
                })
                .ToListAsync();

            return Ok(diseaseHistories);
        }
    }
}