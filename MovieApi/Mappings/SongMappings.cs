using AutoMapper;
using MovieApi.Dtos.Movie;
using MovieApi.Models;
using System.Globalization;

namespace MovieApi.Mappings
{
    public class SongMappings : Profile
    {
        public SongMappings()
        {
            CreateMap<Song, SongDto>()
                .ForMember(songdto => songdto.ReleaseDate, opt => opt.MapFrom(song => song.ReleaseDate.ToString("D")));

            CreateMap<SongCreationDto, Song>()
                .ForMember(song => song.ReleaseDate, opt => opt.MapFrom(songCDto => DateTime.ParseExact(songCDto.ReleaseDate!, "M-d-yyyy", CultureInfo.InvariantCulture)));
        }
    }
}
