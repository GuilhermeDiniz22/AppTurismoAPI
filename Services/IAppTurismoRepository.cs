using AppTurismoAPI.Entities;

namespace AppTurismoAPI.Services
{
    public interface IAppTurismoRepository
    {
        Task<IEnumerable<Cidade>> GetCidades();

        Task<(IEnumerable<Cidade>, Metadados)> GetCidadesAsync(string? nome, string? filtro, int paginaNumero, int paginaTamanho);

        Task<Cidade?> GetCidadeAsync(int cidadeId, bool incluiPontoTuristico);

        Task<IEnumerable<PontoTuristico>> GetPontoTuristicoAsync(int cidadeId);

        Task<bool> CidadeExisteAsync(int cidadeId);

        Task AddCidadeAsync(Cidade cidade);

        Task<PontoTuristico?> GetPontoTuristicoCidadeAsync(int cidadeId, int pontoTuristicoId);

        Task AddPontoTuristico(int cidadeId, PontoTuristico pontoTuristico);

        void DeletarPontoTuristico(PontoTuristico pontoTuristico);

        void DeletarCidade(Cidade cidade);

        Task<bool> Salvar();
    }
}
