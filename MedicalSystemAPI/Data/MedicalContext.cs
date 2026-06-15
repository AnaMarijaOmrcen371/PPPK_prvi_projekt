using Microsoft.EntityFrameworkCore;
using MedicalSystemAPI.Models;

namespace MedicalSystemAPI.Data
{
    public class MedicalContext : DbContext
    {
        public MedicalContext(DbContextOptions<MedicalContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<DiseaseHistory> DiseaseHistories { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Medication> Medications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id = 1,
                    FirstName = "Marko",
                    LastName = "Kovacic",
                    Specialization = "Cardiology"
                },

                new Doctor
                {
                    Id = 2,
                    FirstName = "Ana",
                    LastName = "Horvat",
                    Specialization = "Neurology"
                },

                new Doctor
                {
                    Id = 3,
                    FirstName = "Ivan",
                    LastName = "Babic",
                    Specialization = "Dermatology"
                }
            );
        }
    }
}