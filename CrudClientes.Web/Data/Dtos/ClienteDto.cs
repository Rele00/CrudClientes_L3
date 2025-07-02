using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudClientes.Web.Data.Dtos
{
    public class ClienteDto
    {
        /*
         * ### Modelo de Datos - Cliente
        | Campo | Tipo | Restricciones |
        |-------|------|--------------|
        | Id | int | Primary Key, Identity |
        | Nombre | string | Required, MaxLength(100) |
        | Email | string | Required, Email, MaxLength(100), Unique |
        | Telefono | string | Optional, Phone, MaxLength(15) |
        | Direccion | string | Optional, MaxLength(200) |
        | FechaCreacion | DateTime | Default: DateTime.Now |
        | Activo | bool | Default: true |
        */
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incremental (Identity)
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre Es obligatorio")]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Email es obligatorio")]
        [EmailAddress(ErrorMessage = "El Email no tiene un formato válido")]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "El teléfono no tiene un formato válido")]
        [MaxLength(15)]
        public string? Telefono { get; set; }

        [MaxLength(200)]
        public string? Direccion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;

    }
}
