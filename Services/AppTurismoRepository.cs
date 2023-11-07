using AppTurismoAPI.DbContexts;
using AppTurismoAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppTurismoAPI.Services
{
    public class AppTurismoRepository : IAppTurismoRepository
    {
        private readonly AppTurismoContext _context;

        public AppTurismoRepository(AppTurismoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Cidade?> GetCidadeAsync(int cidadeId, bool incluiPontoTuristico)
        {
            if(incluiPontoTuristico)
            {
                return await _context.Cidades.Include(c => c.PontoTuristicos).Where(c => c.Id == cidadeId).FirstOrDefaultAsync();
            }

            return await _context.Cidades.FirstOrDefaultAsync(c => c.Id == cidadeId);
        }

        public async Task<IEnumerable<Cidade>> GetCidadesAsync()
        {
            return await _context.Cidades.OrderBy(c => c.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Cidade>> GetCidadesAsync(string? nome) //metodo get cidades passando uma query string
        {
            if (string.IsNullOrEmpty(nome))
            {
                return await GetCidadesAsync();
            }

            nome = nome.Trim();
            return await _context.Cidades.Where(c => c.Nome == nome).OrderBy(c => c.Nome).ToListAsync();
        }

        public async Task<IEnumerable<PontoTuristico>> GetPontoTuristicoAsync(int cidadeId)
        {
            return await _context.PontoTuristicos.Where(p => p.CidadeId == cidadeId).ToListAsync();
        }

        public async Task<PontoTuristico?> GetPontoTuristicoCidadeAsync(int cidadeId, int pontoTuristicoId)
        {
            return await _context.PontoTuristicos.Where(p => p.CidadeId == cidadeId && p.Id == pontoTuristicoId).FirstOrDefaultAsync();
        }

        public async Task<bool> CidadeExisteAsync(int cidadeId)
        {
            return await _context.Cidades.AnyAsync(c => c.Id == cidadeId);
        }

        public async Task AddPontoTuristico(int cidadeId, PontoTuristico pontoTuristico)
        {
            var cidade = await GetCidadeAsync(cidadeId, false);

            if(cidade is not null)
            {
                cidade.PontoTuristicos.Add(pontoTuristico);
            }
        }

        public async Task<bool> Salvar()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void DeletarPontoTuristico(PontoTuristico pontoTuristico)
        {
            _context.Remove(pontoTuristico);
        }
    }
}
