using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reserva_de_salas.Models
{
    [Table("Reservas")]
    public class Reserva
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessage = "Selecione um usuário.")]
        public long UsuarioId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        [ValidateNever]
        public Usuario Usuario { get; set; }

        [Required(ErrorMessage = "Selecione uma sala.")]
        public long SalaId { get; set; }

        [ForeignKey(nameof(SalaId))]
        [ValidateNever]
        public Sala Sala { get; set; }

        [Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required, DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan HoraInicio { get; set; }

        [Required, DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan HoraFim { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Informe um número de pessoas válido.")]
        public int NumeroDePessoas { get; set; }
    }
}
