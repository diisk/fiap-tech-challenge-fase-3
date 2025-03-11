using Application.Mappers;
using AutoMapper;
using Contato.Application.DTOs.ContatoDtos;
using Microsoft.Extensions.DependencyInjection;

namespace Contato.Application.Mappers.ContatoMappers
{
    public class AtualizarContatoRequestToContatoMapper : CustomMapper<AtualizarContatoRequest, Domain.Entities.Contato>
    {
        private readonly IMapper mapper;

        public AtualizarContatoRequestToContatoMapper(IMapper mapper) : base(mapper)
        {
            this.mapper = mapper;
        }

        private static List<string> ConverteCidades(string? cidades)
        {
            return string.IsNullOrEmpty(cidades) ? new() : cidades.Split(';').ToList();
        }

        public static void ConfigureMapping(IMapperConfigurationExpression cfg, IServiceCollection services)
        {
            cfg.CreateMap<AtualizarContatoRequest, Domain.Entities.Contato>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) =>
                {
                    if (srcMember == null) return false;
                    if (srcMember.GetType().IsValueType && srcMember.Equals(Activator.CreateInstance(srcMember.GetType())))
                    {
                        return false;
                    }
                    return true;
                }));
            services.AddScoped<AtualizarContatoRequestToContatoMapper>();
        }
    }
}
