using AutoMapper;
using FormulaOne.Entities.DbSet;
using FormulaOne.Entities.Dtos.Response;

namespace FormulaOne.Api.MappingProfiles
{
    public class DomainToRespsone : Profile
    {
        public DomainToRespsone()
        {
            CreateMap<Achivement, DriverAchivementResponse>()
                .ForMember(dest => dest.Wins, opt => opt.MapFrom(src => src.RaceWins));
            //.ForMember(dest => dest.PolePosition, opt => opt.MapFrom(src => src.PolePosition))
            //.ForMember(dest => dest.FastestLap, opt => opt.MapFrom(src => src.FastestLap))
            //.ForMember(dest => dest.WorldChampionship, opt => opt.MapFrom(src => src.WorldChampionship))
            //.ForMember(dest => dest.DriverID, opt => opt.MapFrom(src => src.DriverID));

            CreateMap<Driver, GetDriverResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.Id));

        }   

    }
}
