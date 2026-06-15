using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MedicalSystemAPI.Data
{
    public class MedicalContextFactory : IDesignTimeDbContextFactory<MedicalContext>
    {
        public MedicalContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MedicalContext>();

            optionsBuilder.UseNpgsql(
     "Host=127.0.0.1;Port=5555;Database=medicaldb;Username=medicaluser;Password=medicalpass"
 );

            return new MedicalContext(optionsBuilder.Options);
        }
    }
}