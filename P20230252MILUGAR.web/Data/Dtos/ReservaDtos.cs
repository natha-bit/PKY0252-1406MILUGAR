using P20230252MILUGAR.web.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace P20230252MILUGAR.web.Data.Dtos
{
    public class ReservaDtos
    {
        public int Id { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Estado { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreApartamento { get; set; }
        public string Contacto { get; set; }
        public string UsuarioId { get; set; }
        public int ApartamentoId { get; set; }
        public string Propietario { get; set; } 
    }
}
