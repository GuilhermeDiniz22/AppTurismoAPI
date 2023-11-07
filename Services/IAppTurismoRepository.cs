using AppTurismoAPI.Entities;

namespace AppTurismoAPI.Services
{
    public interface IAppTurismoRepository
    {
        Task<IEnumerable<Cidade>> GetCidadesAsync();

        Task<IEnumerable<Cidade>> GetCidadesAsync(string? nome);

        Task<Cidade?> GetCidadeAsync(int cidadeId, bool incluiPontoTuristico);

        Task<IEnumerable<PontoTuristico>> GetPontoTuristicoAsync(int cidadeId);

        Task<bool> CidadeExisteAsync(int cidadeId);

        Task<PontoTuristico?> GetPontoTuristicoCidadeAsync(int cidadeId, int pontoTuristicoId);

        Task AddPontoTuristico(int cidadeId, PontoTuristico pontoTuristico);

        void DeletarPontoTuristico(PontoTuristico pontoTuristico);

        Task<bool> Salvar();
    }
}
