using AutoMapper;
using MovieApi.Dtos.Actor;
using MovieApi.Models;
using System.Globalization;

namespace MovieApi.Mappings
{
    public class ArtistMappings : Profile
    {
        public ArtistMappings()
        {
            CreateMap<Artist, ArtistDto>()
                .ForMember(artistdto => artistdto.Birthday, opt => opt.MapFrom(artist => artist.Birthday.ToString("D")));

            CreateMap<ArtistCreationDto, Artist>()
                .ForMember(artist => artist.Birthday, opt => opt.MapFrom(artistCDto => DateTime.ParseExact(artistCDto.Birthday!, "M-d-yyyy", CultureInfo.InvariantCulture)));

            CreateMap<ArtistUpdateDto, Artist>();
        }
    }
}
