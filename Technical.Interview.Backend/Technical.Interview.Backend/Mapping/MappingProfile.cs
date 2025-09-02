namespace Technical.Interview.Backend.Mapping;

using AutoMapper;

using Technical.Interview.Backend.Entities;
using Technical.Interview.Backend.Responses;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MarcasAuto, BrandInfo>()
            .ForMember(Prop => Prop.Id, Opt => Opt.MapFrom(Prop => Prop.Id))
            .ForMember(Prop => Prop.Nombre, Opt => Opt.MapFrom(Prop => Prop.Nombre))
            .ForMember(Prop => Prop.SitioWeb, Opt => Opt.MapFrom(Prop => Prop.SitioWeb))
            .ForMember(Prop => Prop.PaisOrigen, Opt => Opt.MapFrom(Prop => Prop.PaisOrigen));
    }
}
