using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppTurismoAPI.Entities
{
    public class Cidade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }
        [MaxLength(150)]
        public string? Descricao { get; set; }

        public ICollection<PontoTuristico> PontoTuristicos { get; set; } = new List<PontoTuristico>();

        public Cidade(string nome)
        {
            Nome = nome;
        }
    }
}
