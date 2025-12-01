using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reserva_de_salas.Models
{
    [Table("Salas")]
    public class Sala
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessage = "Nome da sala é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A capacidade é obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "A capacidade deve ser um número positivo")]
        public int Capacidade { get; set; }

        [StringLength(500, ErrorMessage = "Os recursos devem ter no máximo 500 caracteres")]
        public string Recursos { get; set; }
    }
}
