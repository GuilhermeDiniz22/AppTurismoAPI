using System.Linq;

namespace AppTurismoAPI.Models
{
    public class CidadeDto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string? Descricao { get; set;} 

        public IEnumerable<PontosTuristicosDto> PontosTuristicos { get; set; }
  = new List<PontosTuristicosDto>(); //inicializar icollections para evitar referencias ao null

    }
}
