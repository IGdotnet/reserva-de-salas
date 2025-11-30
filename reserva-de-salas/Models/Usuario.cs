using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reserva_de_salas.Models
{
    [Table("Usuarios")]
    [Index(nameof(Email), IsUnique=true)]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato do email inválido")]
        [StringLength(100, ErrorMessage = "O email deve ter até 100 caracteres")]
        public string Email { get; set; }

        [Required]
        public bool Administrador { get; set; }

    }
}
