using HospitalInformationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalInformationSystem.Processing
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Referral> Referrals => Set<Referral>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=HospitalDB;Trusted_Connection=True;");
            }
        }
    }
}
