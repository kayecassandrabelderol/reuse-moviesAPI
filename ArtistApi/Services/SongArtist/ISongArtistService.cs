namespace MovieApi.Services
{
    public interface ISongArtistService
    {
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
        /// Deletes <paramref name="artistId"/> from <paramref name="songId"/>
        /// </summary>
        /// <param name="songId"></param>
        /// <param name="artistId"></param>
        /// <returns>Returns true if delete is successful, false otherwise</returns>
        Task<bool> DeleteArtistInSong(int songId, int artistId);
    }
}
