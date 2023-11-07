using System.ComponentModel.DataAnnotations;

namespace AppTurismoAPI.Models
{
    public class PontoTuristicoPutDto
    {
        [Required(ErrorMessage = "Você deve preencher o campo nome")]
        [MaxLength(50)]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(200)]
        [Required(ErrorMessage = "o campo descrição é obrigatório.")]
        public string? Descricao { get; set; }
    }
}
