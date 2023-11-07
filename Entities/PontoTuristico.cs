using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppTurismoAPI.Entities
{
    public class PontoTuristico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        [MaxLength(150)]
        public string Descricao { get; set; }

        [ForeignKey("CidadeId")]
        public Cidade? Cidade { get; set; }

        public int CidadeId { get; set; }

        public PontoTuristico(string nome)
        {
            Nome = nome;
        }
    }
}
