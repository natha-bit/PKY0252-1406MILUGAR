using Microsoft.AspNetCore.Identity;

namespace P20230252MILUGAR.web.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Entities.Apartamento> Apartamentos { get; set; } = new List<Entities.Apartamento>();
        public ICollection<Entities.Reserva> Reservas { get; set; } = new List<Entities.Reserva>();
        public string Nombre { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }

}
