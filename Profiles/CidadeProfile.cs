using AppTurismoAPI.Entities;
using AppTurismoAPI.Models;
using AutoMapper;

namespace AppTurismoAPI.Profiles
{
    public class CidadeProfile : Profile // herança de profile do auto mapper
    {
        public CidadeProfile()
        {
            CreateMap<Cidade, CidadeSemPontoTuristicoDto>();
            CreateMap<Cidade, CidadeDto>(0);
        }
    }
}
