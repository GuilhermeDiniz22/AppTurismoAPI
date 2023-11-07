namespace AppTurismoAPI.Models
{
    public class CidadeDto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string? Descricao { get; set;} 

        public int NumeroDePontosTuristicos
        {
            get
            {
                return PontosTuristicos.Count; //retorn a contagem de numero de pontos turisticos
            }
        }


        public ICollection<PontosTuristicosDto> PontosTuristicos { get; set; }
        = new List<PontosTuristicosDto>(); //inicializar icollections para evitar referencias ao null

    }
}
