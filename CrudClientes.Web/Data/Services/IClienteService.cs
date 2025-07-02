using CrudClientes.Web.Data.Dtos;

namespace CrudClientes.Web.Data.Services
{
    public interface IClienteService
    {
        Task<List<ClienteDto>> ObtenerClientesAsync();
        Task<List<ClienteDto>> ObtenerClientesActivosAsync();
        Task<List<ClienteDto>> ObtenerClientesFiltradosActivosAsync(string filtro = "");
        Task<List<ClienteDto>> ObtenerClientesFiltradosAsync(string filtro = "");
        Task CrearClienteAsync(ClienteDto clienteDto);
        Task ActualizarClienteAsync(int id, ClienteDto clienteDto);
        Task EliminarClienteAsync(int id);

    }
}
