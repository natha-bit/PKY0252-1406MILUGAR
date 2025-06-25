using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using P20230252MILUGAR.web.Data.Dtos;
using P20230252MILUGAR.web.Data.Entities;

namespace P20230252MILUGAR.web.Data.Services
{
    public interface IApartamentoServices
    {
        List<ApartamentoDtos> Consultar();
        List<ApartamentoDtos> ConsultarPorUsuarioId(string usuarioId);
        bool Crear(ApartamentoDtos apartamento);
        bool Eliminar(int id);
        bool Modificar(ApartamentoDtos apartamentoDtos);
    }

    public class ApartamentoServices : IApartamentoServices
    {
        private readonly ApplicationDbContext db;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ApartamentoServices(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
        }

        public List<ApartamentoDtos> Consultar()
        {
            return db.Apartamentos
                .Include(a => a.Propietario)
                .Select(a => new ApartamentoDtos
                {
                    Id = a.Id,
                    Titulo = a.Titulo,
                    Descripcion = a.Descripcion,
                    ImagenUrls = a.ImagenUrls,
                    Ciudad = a.Ciudad,
                    Area = a.Area,
                    Direccion = a.Direccion,
                    PrecioMesual = a.PrecioMesual,
                    NumeroHabitaciones = a.NumeroHabitaciones,
                    NumeroBanios = a.NumeroBanios,
                    Disponibilidad = a.Disponibilidad,
                    PropietarioId = a.PropietarioId,
                    ServiciosAdicionales = a.ServiciosAdicionales,
                    PropietarioNombre = a.Propietario != null ? a.Propietario.Nombre : "Desconocido"
                }).ToList();
        }

        public bool Crear(ApartamentoDtos apartamento)
        {
            var nuevoApartamento = new Entities.Apartamento

            {
                Titulo = apartamento.Titulo,
                Descripcion = apartamento.Descripcion,
                ImagenUrls = apartamento.ImagenUrls,
                Direccion = apartamento.Direccion,
                PrecioMesual = apartamento.PrecioMesual,
                NumeroHabitaciones = apartamento.NumeroHabitaciones,
                NumeroBanios = apartamento.NumeroBanios,
                Disponibilidad = true,
                ServiciosAdicionales = apartamento.ServiciosAdicionales,
                Ciudad = apartamento.Ciudad,
                Area = apartamento.Area,
                PropietarioId = apartamento.PropietarioId

            };
            db.Apartamentos.Add(nuevoApartamento);
            return db.SaveChanges() > 0;
        }

        public List<ApartamentoDtos> ConsultarPorUsuarioId(string usuarioId)
        {
            return db.Apartamentos
                .Include(a => a.Propietario)
                .Where(a => a.PropietarioId == usuarioId)
                .Select(a => new ApartamentoDtos
                {
                    Id = a.Id,
                    Titulo = a.Titulo,
                    Descripcion = a.Descripcion,
                    ImagenUrls = a.ImagenUrls,
                    Ciudad = a.Ciudad,
                    Area = a.Area,
                    Direccion = a.Direccion,
                    PrecioMesual = a.PrecioMesual,
                    NumeroHabitaciones = a.NumeroHabitaciones,
                    NumeroBanios = a.NumeroBanios,
                    Disponibilidad = a.Disponibilidad,
                    ServiciosAdicionales = a.ServiciosAdicionales,
                    PropietarioNombre = a.Propietario != null ? a.Propietario.Nombre : "Desconocido"
                }).ToList();
        }


        private Apartamento? Consultar(int id)
        {
            var apartamento = db.Apartamentos.FirstOrDefault(c => c.Id == id);
            return apartamento;
        }

        public bool Modificar(ApartamentoDtos apartamentoDtos)
        {
            var apartamento = Consultar(apartamentoDtos.Id);
            if (apartamento == null)
            {
                return false;
            }
            apartamento.Titulo = apartamentoDtos.Titulo;
            apartamento.Descripcion = apartamentoDtos.Descripcion;
            apartamento.ImagenUrls = apartamentoDtos.ImagenUrls;
            apartamento.Ciudad = apartamentoDtos.Ciudad;
            apartamento.Direccion = apartamentoDtos.Direccion;
            apartamento.PrecioMesual = apartamentoDtos.PrecioMesual;
            apartamento.NumeroHabitaciones = apartamentoDtos.NumeroHabitaciones;
            apartamento.NumeroBanios = apartamentoDtos.NumeroBanios;
            apartamento.Disponibilidad = apartamentoDtos.Disponibilidad;
            apartamento.ServiciosAdicionales = apartamentoDtos.ServiciosAdicionales;
            db.Apartamentos.Update(apartamento);
            return db.SaveChanges() > 0;
        }

        public bool Eliminar(int id)
        {
            var apartamento = Consultar(id);
            if (apartamento == null)
            {
                return false;
            }
            db.Apartamentos.Remove(apartamento);
            return db.SaveChanges() > 0;
        }
    }
}
