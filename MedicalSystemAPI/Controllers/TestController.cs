using Microsoft.AspNetCore.Mvc;
using MedicalSystemAPI.Models;
using MedicalSystemAPI.ORM;

namespace MedicalSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult TestReflection()
        {
            var tableName = SimpleOrmMapper.GetTableName<Patient>();

            var columns = SimpleOrmMapper.GetColumnNames<Patient>();

            return Ok(new
            {
                Table = tableName,
                Columns = columns
            });
        }
    }
}