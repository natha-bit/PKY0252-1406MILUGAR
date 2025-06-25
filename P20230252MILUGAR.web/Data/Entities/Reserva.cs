using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P20230252MILUGAR.web.Data.Entities
{
    [Table("Reservas")]
    public class Reserva
    {
        [Key]
        public int Id { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Estado { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreApartamento { get; set; }
        public string Contacto { get; set; } = null!;   
        public string UsuarioId { get; set; }
        public string Propietario { get; set; }
        public ApplicationUser Usuario { get; set; }
        public int ApartamentoId { get; set; }
        [ForeignKey("ApartamentoId")]
        public Apartamento Apartamento { get; set; }
    }
}
