using CrudClientes.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrudClientes.Web.Data.Context;

public interface IApplicationDbContext
{
    DbSet<Cliente> Clientes { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    // Construir el contexto de la base de datos con las opciones proporcionadas
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    // Definir el DbSet para la entidad Cliente
    public DbSet<Cliente> Clientes { get; set; } = null!;

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}
