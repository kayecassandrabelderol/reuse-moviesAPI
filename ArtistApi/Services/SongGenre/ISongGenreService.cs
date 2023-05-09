namespace ArtistApi.Services
{
    public interface ISongGenreService
    {
        /// <summary>
        /// Checks whether <paramref name="genreId"/> is in <paramref name="songId"/>
        /// </summary>
        /// <param name="songId"></param>
        /// <param name="genreId"></param>
        /// <returns>Returns true if genre exists within a song, false otherwise</returns>
        Task<bool> IsGenreInSong(int songId, int genreId);

        /// <summary>
        /// Inserts <paramref name="genreId"/> to <paramref name="songId"/>
        /// </summary>
        /// <param name="songId"></param>
        /// <param name="genreId"></param>
        /// <returns>Returns true if insert successful, false otherwise</returns>
        Task<bool> AddGenreInSong(int songId, int genreId);

        /// <summary>
        /// Deletes <paramref name="genreId"/> from <paramref name="songId"/>
        /// </summary>
        /// <param name="songId"></param>
        /// <param name="genreId"></param>
        /// <returns>Returns true if delete is successful, false otherwise</returns>
        Task<bool> DeleteGenreInSong(int songId, int genreId);
    }
}
