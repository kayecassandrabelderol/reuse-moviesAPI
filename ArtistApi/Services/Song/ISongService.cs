using MovieApi.Dtos.Movie;
using MovieApi.Models;

namespace MovieApi.Services
{
    public interface ISongService
    {
        /// <summary>
        /// Gets all Songs
        /// </summary>
        /// <returns>Returns all Songs as SongDto</returns>
        Task<IEnumerable<SongDto>> GetAllSongs();

        /// <summary>
        /// Gets all Songs from a given <paramref name="genreId"/>
        /// </summary>
        /// <param name="genreId"></param>
        /// <returns>Returns all Songs from a genre as SongDto</returns>
        Task<IEnumerable<SongDto>> GetAllSongsByGenreId(int genreId);

        /// <summary>
        /// Gets all Songs from a given <paramref name="artistId"/>
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns>Returns all Songs from an artist as SongDto</returns>
        Task<IEnumerable<SongDto>> GetAllSongsByArtistId(int artistId);

        /// <summary>
        /// Gets a single song from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a Songs with all of its Properties as SongByIdDto</returns>
        Task<SongByIdDto?> GetSongById(int id);

        /// <summary>
        /// Gets a single Song from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a Song without IEnumerable Properties</returns>
        Task<SongDto?> GetSongOnly(int id);

        /// <summary>
        /// Creates a new Song from <paramref name="songToCreate"/>
        /// </summary>
        /// <param name="songToCreate"></param>
        /// <returns>Returns the newly created Song as SongDto</returns>
        Task<SongDto> CreateSong(SongCreationDto songToCreate);

        /// <summary>
        /// Updates an existing song record from <paramref name="id"/> given data <paramref name="songToUpdate"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="songToUpdate"></param>
        /// <returns>Returns true if update is successful, false otherwise</returns>
        Task<bool> UpdateSong(int id, SongUpdateDto songToUpdate);

        /// <summary>
        /// Deletes a song from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true if delete is successful, false otherwise</returns>
        Task<bool> DeleteSong(int id);
    }
}
