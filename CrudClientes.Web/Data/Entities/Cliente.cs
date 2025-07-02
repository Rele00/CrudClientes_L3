using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudClientes.Web.Data.Entities
{
    [Table("Clientes")]
    // Nombre de la tabla en la base de datos, se puede dejar vacío para usar el nombre por defecto.
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Nombre { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(15)")]
        public string? Telefono { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? Direccion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        //Por defecto es true
        public bool Activo { get; set; } = true;


    }
}
