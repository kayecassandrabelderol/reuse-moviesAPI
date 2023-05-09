using ArtistApi.Models;

namespace ArtistApi.Contracts
{
    public interface IArtistRepository
    {
        /// <summary>
        /// Gets all Artists
        /// </summary>
        /// <returns>Returns all Artists</returns>
        Task<IEnumerable<Artist>> GetAll();

        /// <summary>
        /// Gets all Artists from the given <paramref name="songId"/>
        /// </summary>
        /// <param name="songId"></param>
        /// <returns>Returns all Artists from a song</returns>
        Task<IEnumerable<Artist>> GetAllBySongId(int songId);

        /// <summary>
        /// Gets a single Artist from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns an Artist</returns>
        Task<Artist?> GetArtist(int id);

        /// <summary>
        /// Creates a new record from a given <paramref name="artist"/>
        /// </summary>
        /// <param name="artist"></param>
        /// <returns>Returns the id of the new artist</returns>
        Task<int> Create(Artist artist);

        /// <summary>
        /// Updates an existing <paramref name="artist"/> record
        /// </summary>
        /// <param name="artist"></param>
        /// <returns>Returns true if update is successful, false otherwise</returns>
        Task<bool> Update(Artist artist);

        /// <summary>
        /// Deletes an artist from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true if delete is successful, false otherwise</returns>
        Task<bool> Delete(int id);
    }
}
