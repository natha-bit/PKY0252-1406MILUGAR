using Microsoft.EntityFrameworkCore;
using P20230252MILUGAR.web.Data.Dtos;
using P20230252MILUGAR.web.Data.Entities;

namespace P20230252MILUGAR.web.Data.Services
{
    public interface IReservaServices
    {
        int Crear(ReservaDtos reserva);
        Task<ReservaDtos?> ObtenerReservaPorIdAsync(int id);
        Task<List<ReservaDtos>> CosultarId(string usuarioId);
        Task<List<ReservaDtos>> ConsultarPorApartamentoIdAsync(int apartamentoId);
        Task<bool> ActualizarEstadoReservaAsync(int reservaId, string nuevoEstado);
        Task<List<ReservaDtos>> ConsultarPorPropietario(string nombrePropietario);
        Task<int> ObtenerNotificacionesAsync(string userId, string rol, string nombreUsuario);
        Task<List<ReservaDtos>> ConsultarTodasAsync();



    }

    public class ReservaServices : IReservaServices
    {
        private readonly ApplicationDbContext _context;

        public ReservaServices(ApplicationDbContext db)
        {
            _context = db;
        }

        public int Crear(ReservaDtos reserva)
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
            _context.SaveChanges();
            return nuevaReserva.Id;
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
        public async Task<List<ReservaDtos>> ConsultarPorApartamentoIdAsync(int apartamentoId)
        {
            return await _context.Reservas
                .Where(r => r.ApartamentoId == apartamentoId)
                .Select(r => new ReservaDtos
                {
                    Id = r.Id,
                    FechaSolicitud = r.FechaSolicitud,
                    Estado = r.Estado,
                    NombreUsuario = r.NombreUsuario,
                    Contacto = r.Contacto,
                    Propietario = r.Propietario,
                    ApartamentoId = r.ApartamentoId,
                    UsuarioId = r.UsuarioId,
                    NombreApartamento = r.NombreApartamento
                })
                .ToListAsync();
        }

        public async Task<bool> ActualizarEstadoReservaAsync(int reservaId, string nuevoEstado)
        {
            var reserva = await _context.Reservas.FindAsync(reservaId);
            if (reserva == null)
                return false;

            reserva.Estado = nuevoEstado;
            _context.Reservas.Update(reserva);

            // Si se cancela, poner el apartamento como disponible
            if (nuevoEstado == "Cancelada")
            {
                var apartamento = await _context.Apartamentos.FindAsync(reserva.ApartamentoId);
                if (apartamento != null)
                {
                    apartamento.Disponibilidad = true;
                    _context.Apartamentos.Update(apartamento);
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<ReservaDtos>> ConsultarPorPropietario(string nombrePropietario)
        {
            return await _context.Reservas
                .Where(r => r.Propietario == nombrePropietario)
                .Select(r => new ReservaDtos
                {
                    Id = r.Id,
                    Estado = r.Estado,
                    FechaSolicitud = r.FechaSolicitud,
                    NombreApartamento = r.NombreApartamento,
                    NombreUsuario = r.NombreUsuario,
                    UsuarioId = r.UsuarioId,
                    ApartamentoId = r.ApartamentoId
                }).ToListAsync();
        }

        public async Task<int> ObtenerNotificacionesAsync(string userId, string rol, string nombreUsuario)
        {
            if (rol == "Cliente")
            {
                return await _context.Reservas
                    .Where(r => r.UsuarioId == userId && (r.Estado == "Aceptada" || r.Estado == "Cancelada"))
                    .CountAsync();
            }
            else if (rol == "Vendedor")
            {
                return await _context.Reservas
                    .Where(r => r.Propietario == nombreUsuario && r.Estado == "Pendiente")
                    .CountAsync();
            }

            return 0;
        }

        public async Task<List<ReservaDtos>> ConsultarTodasAsync()
        {
            return await _context.Reservas
                .Select(r => new ReservaDtos
                {
                    Id = r.Id,
                    Estado = r.Estado,
                    ApartamentoId = r.ApartamentoId,
                    // otros campos si quieres
                })
                .ToListAsync();
        }


    }

}
