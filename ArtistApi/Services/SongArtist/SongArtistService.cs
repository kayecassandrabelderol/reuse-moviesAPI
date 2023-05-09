using ArtistApi.Contracts;

namespace ArtistApi.Services
{
    public class SongArtistService : ISongArtistService
    {
        private readonly ISongRepository _songRepository;

        public SongArtistService(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task<bool> AddArtistInSong(int songId, int artistId)
        {
            return await _songRepository.AddArtistInSong(songId, artistId);
        }

        public async Task<bool> DeleteArtistInSong(int songId, int artistId)
        {
            return await _songRepository.DeleteArtistInSong(songId, artistId);
        }

        public async Task<bool> IsArtistInSong(int songId, int artistId)
        {
            return await _songRepository.IsArtistInSong(songId, artistId);
        }
    }
}
