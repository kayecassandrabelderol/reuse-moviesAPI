using MovieApi.Models;

namespace MovieApi.Contracts
{
    public interface ISongRepository
    {
        /// <summary>
        /// Gets all Songs
        /// </summary>
        /// <returns>Returns all Songs</returns>
        Task<IEnumerable<Song>> GetAll();

        /// <summary>
        /// Gets all Songs from the given <paramref name="genreId"/>
        /// </summary>
        /// <param name="genreId"></param>
        /// <returns>Returns all Songs from a genre</returns>
        Task<IEnumerable<Song>> GetAllByGenreId(int genreId);

        /// <summary>
        /// Gets all Songs from the given <paramref name="artistId"/>
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns>Returns all Songs from an actor</returns>
        Task<IEnumerable<Song>> GetAllByArtistId(int artistId);

        /// <summary>
        /// Gets a single Song from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a Song with all of its Properties</returns>
        Task<Song?> GetSong(int id);

        /// <summary>
        /// Gets a single Song from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a Song without IEnumerable Properties</returns>
        Task<Song?> GetSongOnly(int id);

        /// <summary>
        /// Creates a new record from a given <paramref name="song"/>
        /// </summary>
        /// <param name="song"></param>
        /// <returns>Returns the new Song</returns>
        Task<int> Create(Song song);

        /// <summary>
        /// Checks whether <paramref name="artistId"/> is in <paramref name="songId"/>
        /// </summary>
        /// <param name="songId"></param>
        /// <param name="artistId"></param>
        /// <returns>Returns true if artist exists within a song, false otherwise</returns>
        Task<bool> IsArtistInSong(int songId, int artistId);

        /// <summary>
        /// Inserts <paramref name="artistId"/> to <paramref name="songId"/>
        /// </summary>
        /// <param name="songId"></param>
        /// <param name="artistId"></param>
        /// <returns>Returns true if insert successful, false otherwise</returns>
        Task<bool> AddArtistInSong(int songId, int artistId);

        /// <summary>
        /// Checks whether <paramref name="genreId"/> is in <paramref name="songId"/>
        /// </summary>
        /// <param name="songId"></param>
        /// <param name="genreId"></param>
        /// <returns>Returns true if genre exists within a movie, false otherwise</returns>
        Task<bool> IsGenreInSong(int songId, int genreId);

        /// <summary>
        /// Inserts <paramref name="genreId"/> to <paramref name="songId"/>
        /// </summary>
        /// <param name="songId"></param>
        /// <param name="genreId"></param>
        /// <returns>Returns true if insert successful, false otherwise</returns>
        Task<bool> AddGenreInSong(int songId, int genreId);

        /// <summary>
        /// Updates an existing <paramref name="song"/> record
        /// </summary>
        /// <param name="song"></param>
        /// <returns>Returns true if update is successful, false otherwise</returns>
        Task<bool> Update(Song song);

        /// <summary>
        /// Deletes a song from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true if delete is successful, false otherwise</returns>
        Task<bool> Delete(int id);

        /// <summary>
        /// Deletes <paramref name="genreId"/> from <paramref name="songId"/>
        /// </summary>
        /// <param name="songId"></param>
        /// <param name="genreId"></param>
        /// <returns>Returns true if delete is successful, false otherwise</returns>
        Task<bool> DeleteGenreInSong(int songId, int genreId);

        /// <summary>
        /// Deletes <paramref name="artistId"/> from <paramref name="songId"/>
        /// </summary>
        /// <param name="songId"></param>
        /// <param name="artistId"></param>
        /// <returns>Returns true if delete is successful, false otherwise</returns>
        Task<bool> DeleteArtistInSong(int songId, int artistId);
    }
}
