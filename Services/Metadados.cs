namespace AppTurismoAPI.Services
{
    public class Metadados
    {
        public int TotalItens { get; set; }

        public int TotalPaginas { get; set;}

        public int TamanhoPagina { get; set;}

        public int PaginaAtual { get; set;}

        public Metadados(int totalItens, int tamanhoPagina, int paginaAtual)
        {
            TotalItens = totalItens;
            TotalPaginas =(int) Math.Ceiling(totalItens / (double)tamanhoPagina);
            TamanhoPagina = tamanhoPagina;
            PaginaAtual = paginaAtual;
        }
    }
}
