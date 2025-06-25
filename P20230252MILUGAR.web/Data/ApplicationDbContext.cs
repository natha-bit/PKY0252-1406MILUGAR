using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using P20230252MILUGAR.web.Data.Entities;

namespace P20230252MILUGAR.web.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Apartamento> Apartamentos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Apartamento>()
                .HasOne(a => a.Propietario)
                .WithMany(u => u.Apartamentos)
                .HasForeignKey(a => a.PropietarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.Reservas)
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
