using Microsoft.EntityFrameworkCore;

namespace CodeFirstWithDatabase.Model
{
    public class CMSContext : DbContext
    {
        public CMSContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Laboratory> Laboratories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Registrar> Registrars { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        
    }
}
