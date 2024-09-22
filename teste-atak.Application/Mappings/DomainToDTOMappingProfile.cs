using AutoMapper;
using teste_atak.Application.DTOs;
using teste_atak.Domain.Entities;

namespace teste_atak.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Customer, CustomerDTO>().ReverseMap();
        }
    }
}
