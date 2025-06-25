using Microsoft.EntityFrameworkCore;
using P20230252MILUGAR.web.Data.Dtos;
using P20230252MILUGAR.web.Data.Entities;

namespace P20230252MILUGAR.web.Data.Services
{
    public interface IReservaServices
    {
        bool Crear(ReservaDtos reserva);
        Task<ReservaDtos?> ObtenerReservaPorIdAsync(int id);
        Task<List<ReservaDtos>> CosultarId(string usuarioId);

    }

    public class ReservaServices : IReservaServices
    {
        private readonly ApplicationDbContext _context;

        public ReservaServices(ApplicationDbContext db)
        {
            _context = db;
        }

        public bool Crear(ReservaDtos reserva)
        {
            var nuevaReserva = new Reserva
            {
                FechaSolicitud = reserva.FechaSolicitud,
                Estado = reserva.Estado,
                NombreUsuario = reserva.NombreUsuario,
                NombreApartamento = reserva.NombreApartamento,
                Contacto = reserva.Contacto,
                UsuarioId = reserva.UsuarioId,
                Propietario = reserva.Propietario,
                ApartamentoId = reserva.ApartamentoId
            };

            var apartamento = _context.Apartamentos.FirstOrDefault(a => a.Id == reserva.ApartamentoId);
            if (apartamento != null)
            {
                apartamento.Disponibilidad = false;
                _context.Apartamentos.Update(apartamento);
                _context.SaveChangesAsync();
            }
            _context.Reservas.Add(nuevaReserva);
            return _context.SaveChanges() > 0;
        }

        public async Task<ReservaDtos?> ObtenerReservaPorIdAsync(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);

            if (reserva == null)
                return null;

            return new ReservaDtos
            {
                Id = reserva.Id,
                Estado = reserva.Estado,
                FechaSolicitud = reserva.FechaSolicitud,
                ApartamentoId = reserva.ApartamentoId,
                NombreUsuario = reserva.NombreUsuario,
                Contacto = reserva.Contacto,
                Propietario = reserva.Propietario,
                NombreApartamento = reserva.NombreApartamento,
                UsuarioId = reserva.UsuarioId
            };
        }
        public async Task<List<ReservaDtos>> CosultarId(string usuarioId)
        {
            return await _context.Reservas
                .Where(r => r.UsuarioId == usuarioId)
                .Select(r => new ReservaDtos
                {
                    Id = r.Id,
                    FechaSolicitud = r.FechaSolicitud,
                    Estado = r.Estado,
                    NombreApartamento = r.NombreApartamento,
                    ApartamentoId = r.ApartamentoId,
                    UsuarioId = r.UsuarioId
                })
                .ToListAsync();
        }


    }

}
