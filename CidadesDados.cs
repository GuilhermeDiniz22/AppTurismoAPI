using AppTurismoAPI.Models;

using System.Collections.Generic;

namespace AppTurismoAPI.Models
{
    public class CidadesDados
    {
        public List<CidadeDto> Cidades { get; set; }

        //public static CidadesDados Atual { get; } = new CidadesDados();

        public CidadesDados()
        {
            Cidades = new List<CidadeDto>()
            {
                new CidadeDto()
                {
                    Id = 1,
                    Nome = "Recife",
                    Descricao = "A Veneza Brasileira."
                },

                new CidadeDto()
                {
                    Id = 2,
                    Nome = "Jaboatão dos Guararapes",
                    Descricao = "A pátria nasceu aqui.",
                    PontosTuristicos = new List<PontosTuristicosDto>()
                    {
                        new PontosTuristicosDto()
                        {
                            Id = 1,
                            Nome = "Igrejinha de piedade",
                            Descricao = "A Igreja de Nossa Senhora da Piedade em estilo maneirista, situa-se à beira mar de Piedade tendo em seu entorno construções " +
                            "de edifícios altos, bares e restaurantes. Sofreu várias reformas e melhoramentos ao longo da sua história. " +
                            "É uma construção em alvenaria de pedra, com anexo de um convento erguido no século XVIII. " +
                            "Encontra-se em regular estado de conservação, sendo tombada por lei municipal e em nível Federal."
                        },
                        new PontosTuristicosDto()
                        {
                            Id  = 2,
                            Nome = "O Parque Histórico Nacional dos Guararapes.",
                            Descricao = "É um dos mais importantes locais históricos do Brasil. " +
                            "Foi palco das batalhas decisivas travadas entre luso-brasileiros e holandeses, " +
                            "na Guerra da Restauração Pernambucana. Nas encostas dos seus morros, os holandeses foram derrotados " +
                            "em 1648 e 1649 pelos nossos combatentes. Após a rendição dos flamengos, o Governador de Pernambuco, " +
                            "Francisco Barreto de Meneses, mandou erguer uma capela votiva a Nossa Senhora dos Prazeres, em ação de graças " +
                            "pelas duas vitórias alcançadas."
                        }
                    }
                },

                new CidadeDto()
                {
                    Id = 3,
                    Nome = "Olinda",
                    Descricao = "A cidade do carnaval."
                }
            };
        }
    }
}
