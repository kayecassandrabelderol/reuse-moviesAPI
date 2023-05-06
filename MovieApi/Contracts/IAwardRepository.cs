using MovieApi.Models;

namespace MovieApi.Contracts
{
    public interface IAwardRepository
    {
        /// <summary>
        /// Gets all Awards
        /// </summary>
        /// <returns>Returns all Awards</returns>
        Task<IEnumerable<Award>> GetAll();

        /// <summary>
        /// Gets all Awards from the given <paramref name="songId"/>
        /// </summary>
        /// <param name="songId"></param>
        /// <returns>Returns all Awards from a song</returns>
        Task<IEnumerable<Award>> GetAllBySongId(int songId);

        /// <summary>
        /// Gets a single Award from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns an Award</returns>
        Task<Award?> GetAward(int id);

        /// <summary>
        /// Creates a new record from a given <paramref name="award"/>
        /// </summary>
        /// <param name="award"></param>
        /// <returns>Returns the id of the new Award</returns>
        Task<int> Create(Award award);

        /// <summary>
        /// Updates an existing <paramref name="award"/> record
        /// </summary>
        /// <param name="award"></param>
        /// <returns>Returns true if update is successful, false otherwise</returns>
        Task<bool> Update(Award award);

        /// <summary>
        /// Deletes an award from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true if delete is successful, false otherwise</returns>
        Task<bool> Delete(int id);
    }
}
