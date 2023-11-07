using AppTurismoAPI.Entities;
using AppTurismoAPI.Models;
using AutoMapper;

namespace AppTurismoAPI.Profiles
{
    public class PontoTuristicoProfile : Profile
    {
        public PontoTuristicoProfile()
        {
            CreateMap<PontoTuristico, PontosTuristicosDto>();
            CreateMap<PontoTuristicoPostDto, PontoTuristico>();
            CreateMap<PontoTuristicoPutDto, PontoTuristico>();
            CreateMap<PontoTuristico, PontoTuristicoPutDto>();

        }
    }
}
