using MovieApi.Contracts;

namespace MovieApi.Services
{
    public class SongGenreService : ISongGenreService
    {
        private readonly ISongRepository _songRepository;

        public SongGenreService(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task<bool> AddGenreInSong(int songId, int genreId)
        {
            return await _songRepository.AddGenreInSong(songId, genreId);
        }

        public async Task<bool> DeleteGenreInSong(int songId, int genreId)
        {
            return await _songRepository.DeleteGenreInSong(songId, genreId);
        }

        public async Task<bool> IsGenreInSong(int songId, int genreId)
        {
            return await _songRepository.IsGenreInSong(songId, genreId);
        }
    }
}
