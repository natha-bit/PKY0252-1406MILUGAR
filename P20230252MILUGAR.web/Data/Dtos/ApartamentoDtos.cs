using System.ComponentModel.DataAnnotations;

namespace P20230252MILUGAR.web.Data.Dtos
{
    public class ApartamentoDtos
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string ImagenUrls { get; set; } = null!;
        public string Ciudad { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public decimal PrecioMesual { get; set; }
        public int NumeroHabitaciones { get; set; }
        public int NumeroBanios { get; set; }
        public int Area { get; set; }
        public bool Disponibilidad { get; set; }
        public string ServiciosAdicionales { get; set; } = null!;
        public string PropietarioId { get; set; } = null!;
        public string PropietarioNombre { get; set; } = null!;


    }
}
