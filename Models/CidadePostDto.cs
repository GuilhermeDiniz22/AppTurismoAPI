using System.ComponentModel.DataAnnotations;

namespace AppTurismoAPI.Models
{
    public class CidadePostDto
    {
        [Required(ErrorMessage = "Você deve preencher o campo nome")]
        [MaxLength(50)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Você deve preencher o campo nome")]
        [MaxLength(200)]
        public string? Descricao { get; set; }
    }
}
