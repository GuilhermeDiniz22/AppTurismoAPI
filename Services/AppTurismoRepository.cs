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

        public async Task<IEnumerable<Cidade>> GetCidades()
        {
            return await _context.Cidades.OrderBy(c => c.Nome).ToListAsync();
        }

        public async Task<(IEnumerable<Cidade>, Metadados)> GetCidadesAsync(string? nome, string? filtro, int paginaNumero, int paginaTamanho) //metodo get cidades passando uma query string
        {
           
            var colecao = _context.Cidades as IQueryable<Cidade>;

            if (!string.IsNullOrEmpty(nome))
            {
                nome = nome.Trim();
                colecao = colecao.Where(c => c.Nome == nome);
            }

            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.Trim();
                colecao = colecao.Where(c => c.Nome.Contains(filtro) || (c.Descricao != null && c.Descricao.Contains(filtro)));
            }
            var totalItens = await colecao.CountAsync();

            var metadados = new Metadados(totalItens, paginaTamanho, paginaNumero);

            var colecaoRetorno = await colecao.OrderBy(c => c.Nome).Skip(paginaTamanho * (paginaNumero - 1))
                .Take(paginaTamanho).ToListAsync();

            return (colecaoRetorno, metadados);

        }

        public async Task AddCidadeAsync(Cidade cidade)
        {
           await _context.Cidades.AddAsync(cidade);        
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

        public void DeletarCidade(Cidade cidade)
        {
            _context.Remove(cidade);
        }
    }
}
