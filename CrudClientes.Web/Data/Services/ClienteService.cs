using CrudClientes.Web.Data.Context;
using CrudClientes.Web.Data.Dtos;
using CrudClientes.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace CrudClientes.Web.Data.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IApplicationDbContext _context;

        public ClienteService(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task ActualizarClienteAsync(int id, ClienteDto dto)
        {
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == id)
                .ConfigureAwait(true);

            if (cliente == null) return; // Solo valida si existe

            // Actualizar los campos del cliente con los valores del DTO
            if (cliente.Nombre != dto.Nombre)
            {
                cliente.Nombre = dto.Nombre;
            }
            if (cliente.Email != dto.Email)
            {
                cliente.Email = dto.Email;
            }
            if (cliente.Telefono != dto.Telefono)
            {
                cliente.Telefono = dto.Telefono;
            }
            if (cliente.Direccion != dto.Direccion)
            {
                cliente.Direccion = dto.Direccion;
            }
            if (cliente.Activo != dto.Activo)
            {
                cliente.Activo = dto.Activo;
            }
            // ¡No se debe cambiar la fecha de creación, esto es una modificación!
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync().ConfigureAwait(true);
        }

        public async Task<List<ClienteDto>> ObtenerClientesActivosAsync()
        {
            return await _context.Clientes
                .Where(c => c.Activo)
                .AsNoTracking()
                .Select(c => new ClienteDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Email = c.Email,
                    Telefono = c.Telefono,
                    Direccion = c.Direccion,
                    FechaCreacion = c.FechaCreacion
                })
                .ToListAsync()
                .ConfigureAwait(true);
        }

        public async Task<List<ClienteDto>> ObtenerClientesAsync()
        {
            return await _context.Clientes
                .AsNoTracking()
                .Select(c => new ClienteDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Email = c.Email,
                    Telefono = c.Telefono,
                    Direccion = c.Direccion,
                    FechaCreacion = c.FechaCreacion
                })
                .ToListAsync()
                .ConfigureAwait(true);
        }

        // Implementación de la sobrecarga con un parámetro de filtro  
        public async Task<List<ClienteDto>> ObtenerClientesFiltradosAsync(string filtro)
        {
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                string filtroLower = filtro.ToLower();
                bool filtroEsNumero = int.TryParse(filtro, out int id_en_filtro);

                return await _context.Clientes
                    .AsNoTracking()
                    .Where(c =>
                        EF.Functions.Like(c.Nombre.ToLower(), $"%{filtroLower}%") ||
                        EF.Functions.Like(c.Email.ToLower(), $"%{filtroLower}%") ||
                        (filtroEsNumero && c.Id == id_en_filtro) ||
                        EF.Functions.Like(c.Telefono, $"%{filtro}%")
                    )
                    .Select(c => new ClienteDto
                    {
                        Id = c.Id,
                        Nombre = c.Nombre,
                        Email = c.Email,
                        Telefono = c.Telefono,
                        Direccion = c.Direccion,
                        FechaCreacion = c.FechaCreacion
                    })
                    .ToListAsync()
                    .ConfigureAwait(true);
            }

            // Si el filtro está vacío o es nulo, devolver todos los clientes activos
            return await ObtenerClientesActivosAsync().ConfigureAwait(true);
        }
        public async Task<List<ClienteDto>> ObtenerClientesFiltradosActivosAsync(string filtro)
        {
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                string filtroLower = filtro.ToLower();
                bool filtroEsNumero = int.TryParse(filtro, out int id_en_filtro);

                return await _context.Clientes
                    .AsNoTracking()
                    .Where(c => c.Activo && (
                        EF.Functions.Like(c.Nombre.ToLower(), $"%{filtroLower}%") ||
                        EF.Functions.Like(c.Email.ToLower(), $"%{filtroLower}%") ||
                        (filtroEsNumero && c.Id == id_en_filtro) ||
                        EF.Functions.Like(c.Telefono, $"%{filtro}%")
                    ))
                    .Select(c => new ClienteDto
                    {
                        Id = c.Id,
                        Nombre = c.Nombre,
                        Email = c.Email,
                        Telefono = c.Telefono,
                        Direccion = c.Direccion,
                        FechaCreacion = c.FechaCreacion
                    })
                    .ToListAsync()
                    .ConfigureAwait(true);
            }

            // Si el filtro está vacío o es nulo, devolver todos los clientes activos
            return await ObtenerClientesActivosAsync().ConfigureAwait(true);
        }

        public async Task CrearClienteAsync(ClienteDto dto)
        {
            var cliente = new Cliente
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                FechaCreacion = DateTime.Now,
                Activo = dto.Activo // Por defecto, el cliente debe estar activo al crearlo. Toma el valor real del formulario con el dto y se lo pasa a Activo
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync().ConfigureAwait(true);
        }

        public async Task EliminarClienteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id).ConfigureAwait(true);

            if (cliente == null)
                return;

            // Si se desea implementar una eliminación lógica, se puede descomentar la siguiente línea:
            // cliente.Activo = false;
            // _context.Clientes.Update(cliente);

            // Esta es la eliminación física
            _context.Clientes.Remove(cliente);
            bool registrado = await _context.SaveChangesAsync().ConfigureAwait(true) > 0;

            if (!registrado)
            {
                Console.WriteLine("No se pudo eliminar el cliente.");
                return;
            }

            // await _context.SaveChangesAsync().ConfigureAwait(true);
        }
    }
}