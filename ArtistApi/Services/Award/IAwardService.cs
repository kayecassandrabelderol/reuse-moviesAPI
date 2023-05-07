using MovieApi.Dtos.Award;
using MovieApi.Models;

namespace MovieApi.Services
{
    public interface IAwardService
    {
        /// <summary>
        /// Gets all Awards
        /// </summary>
        /// <returns>Returns all Awards as AwardDto</returns>
        Task<IEnumerable<AwardDto>> GetAllAwards();

        /// <summary>
        /// Gets all awards from a given <paramref name="songId"/>
        /// </summary>
        /// <param name="songId"></param>
        /// <returns>Returns all Awards from a Song as AwardDto</returns>
        Task<IEnumerable<AwardDto>> GetAllAwards(int songId);

        /// <summary>
        /// Gets a single Award from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns an award as AwardDto</returns>
        Task<AwardDto?> GetAwardById(int id);

        /// <summary>
        /// Creates a new Award from <paramref name="awardToCreate"/>
        /// </summary>
        /// <param name="awardToCreate"></param>
        /// <returns>Returns the newly created Award as AwardDto</returns>
        Task<AwardDto> CreateAward(AwardCreationDto awardToCreate);

        /// <summary>
        /// Updates an existing award record from <paramref name="id"/> given data <paramref name="awardToUpdate"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="awardToUpdate"></param>
        /// <returns>Returns true if update is successful, false otherwise</returns>
        Task<bool> UpdateAward(int id, AwardUpdateDto awardToUpdate);

        /// <summary>
        /// Deletes an artist from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true if delete is successful, false otherwise</returns>
        Task<bool> DeleteAward(int id);
    }
}
