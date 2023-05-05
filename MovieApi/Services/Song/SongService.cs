using AutoMapper;
using MovieApi.Contracts;
using MovieApi.Dtos.Movie;
using MovieApi.Models;
using System.Globalization;

namespace MovieApi.Services
{
    public class SongService : ISongService
    {
        private readonly ISongRepository _songRepository;
        private readonly IMapper _mapper;

        public SongService(ISongRepository repository, IMapper mapper)
        {
            _songRepository = repository;
            _mapper = mapper;
        }

        public async Task<SongDto> CreateSong(SongCreationDto songToCreate)
        {
            var songModel = _mapper.Map<Song>(songToCreate);

            songModel.Id = await _songRepository.Create(songModel);

            return _mapper.Map<SongDto>(songModel);
        }

        public async Task<bool> DeleteSong(int id)
        {
            return await _songRepository.Delete(id);
        }

        public async Task<IEnumerable<SongDto>> GetAllSongs()
        {
            var songModels = await _songRepository.GetAll();

            return _mapper.Map<IEnumerable<SongDto>>(songModels);
        }

        public async Task<IEnumerable<SongDto>> GetAllSongsByArtistId(int artistId)
        {
            var songModels = await _songRepository.GetAllByActorId(artistId);

            return _mapper.Map<IEnumerable<SongDto>>(songModels);
        }

        public async Task<IEnumerable<SongDto>> GetAllSongsByGenreId(int genreId)
        {
            var songModels = await _songRepository.GetAllByGenreId(genreId);

            return _mapper.Map<IEnumerable<SongDto>>(songModels);
        }

        public async Task<SongByIdDto?> GetSongById(int id)
        {
            var songModel = await _songRepository.GetSong(id);
            if (songModel == null) return null;

            var ret = new SongByIdDto
            {
                Id = songModel.Id,
                Title = songModel.Title,
                Duration = songModel.Duration,
                ReleaseDate = songModel.ReleaseDate.ToString("D")
            };

            var nonNullMovies = songModel.Genres.Where(x => x != null).ToList();
            var nonNullActors = songModel.Actors.Where(x => x != null).ToList();
            var nonNullAwards = songModel.Awards.Where(x => x != null).ToList();

            if (nonNullMovies.Any())
            {
                var songNames = nonNullMovies.Select(x => x.Name).ToList();
                ret.Genres = songNames.Distinct().ToList();
            }

            if (nonNullActors.Any())
            {
                var artistNames = nonNullActors.Select(x => x.Name).ToList();
                ret.Actors = artistNames.Distinct().ToList();
            }

            if (nonNullAwards.Any())
            {
                var awardNames = nonNullAwards.Select(x => x.Name).ToList();
                ret.Awards = awardNames.Distinct().ToList();
            }

            return ret;
        }

        public async Task<SongDto?> GetSongOnly(int id)
        {
            var songModel = await _songRepository.GetSongOnly(id);
            if (songModel == null) return null;

            return _mapper.Map<SongDto>(songModel);
        }

        public async Task<bool> UpdateSong(int id, SongUpdateDto songToUpdate)
        {
            var songModel = new Song
            {
                Id = id,
                ReleaseDate = DateTime.ParseExact(songToUpdate.ReleaseDate!, "M-d-yyyy", CultureInfo.InvariantCulture)
            };

            return await _songRepository.Update(songModel);
        }
    }
}
