using Microsoft.AspNetCore.Mvc;
using MedicalSystemAPI.Helpers;
using MedicalSystemAPI.Models;

namespace MedicalSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReflectionTestController : ControllerBase
    {
        [HttpGet]
        public IActionResult TestReflection()
        {
            Patient patient = new Patient
            {
                FirstName = "Iva",
                LastName = "Brezovic",
                OIB = "12345678901",
                DateOfBirth = new DateTime(2000, 5, 10),
                Gender = "Female",
                ResidenceAddress = "Zagreb",
                LivingAddress = "Zagreb"
            };

            string sql = ReflectionOrm.GenerateInsertQuery(patient);

            return Ok(sql);
        }
    }
}