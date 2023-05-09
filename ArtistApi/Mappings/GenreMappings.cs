using Api.Dtos.Genre;
using AutoMapper;
using ArtistApi.Dtos.Genre;
using ArtistApi.Models;

namespace ArtistApi.Mappings
{
    public class GenreMappings : Profile
    {
        public GenreMappings()
        {
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreCreationDto, Genre>();
            CreateMap<GenreUpdateDto, Genre>();
        }
    }
}
