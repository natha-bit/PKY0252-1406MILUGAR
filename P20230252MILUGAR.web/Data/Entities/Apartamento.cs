using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P20230252MILUGAR.web.Data.Entities
{
    [Table("Apartamentos")]
    public class Apartamento
    {
        [Key]
        public int Id { get; set; }
        public String Titulo { get; set; } = null!;
        public String Descripcion { get; set; } = null!;
        public string ImagenUrls { get; set; } = null!;
        public string Ciudad { get; set; } = null!;
        public String Direccion { get; set; } = null!;
        public decimal PrecioMesual { get; set; }
        public int NumeroHabitaciones { get; set; }
        public int NumeroBanios { get; set; }
        public int Area { get; set; }
        public bool Disponibilidad { get; set; }
        public string ServiciosAdicionales { get; set; } = null!;
        public string PropietarioId { get; set; } = null!;
        public ApplicationUser Propietario { get; set; } = null!;
    }
}
