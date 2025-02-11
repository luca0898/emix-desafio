using AutoMapper;
using eMix.ConsultaCEP.Models;

namespace eMix.ConsultaCEP.Configurations.MapProfiles
{
    public class ViaCepProfile : Profile
    {
        public ViaCepProfile()
        {
            CreateMap<ViaCepRequestResult, Address>()
                .ForMember(dest => dest.Cep, o => o.MapFrom(src => src.cep.Replace("-", "")));
        }
    }
}
