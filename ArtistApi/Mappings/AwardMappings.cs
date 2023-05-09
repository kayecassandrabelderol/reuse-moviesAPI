using AutoMapper;
using ArtistApi.Dtos.Award;
using ArtistApi.Models;

namespace ArtistApi.Mappings
{
    public class AwardMappings : Profile
    {
        public AwardMappings()
        {
            CreateMap<Award, AwardDto>();
            CreateMap<AwardCreationDto, Award>();
            CreateMap<AwardUpdateDto, Award>();
        }
    }

}
