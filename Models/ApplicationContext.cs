using Microsoft.EntityFrameworkCore;
using QwiikTechnicalTest.Models.DB;

namespace QwiikTechnicalTest.Models
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(a => a.Customer)
                      .WithMany(c => c.Appointments)
                      .HasForeignKey(a => a.CustomerId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => new
                {
                    e.AppointmentDate,
                    e.AppointmentTime
                })
               .IsUnique()
               .HasDatabaseName("ux_appointment_slot");
            });
        }

    }
}
