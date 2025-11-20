using AutoMapper;
using FormulaOne.Entities.DbSet;
using FormulaOne.Entities.Dtos.Request;

namespace FormulaOne.Api.MappingProfiles
{
    public class RequestToDomain: Profile
    {
        public RequestToDomain() 
        {
            CreateMap<CreateDriverAchivementRequest, Achivement>()
                .ForMember(dest => dest.RaceWins, opt => opt.MapFrom(src => src.Wins))
                .ForMember(dest => dest.AddedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1));

            CreateMap<UpdateDriverAchivementRequest, Achivement>()
                .ForMember(dest => dest.RaceWins, opt => opt.MapFrom(src => src.Wins))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<CreateDriverRequest,Driver>()
                .ForMember(dest => dest.AddedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1));

            CreateMap<UpdateDriverRequest, Driver>()
                //.ForMember(dest => dest.AddedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
                //.ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1));
        }
    }
}
