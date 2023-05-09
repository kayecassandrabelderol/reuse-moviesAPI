using ArtistApi.Dtos.Actor;

namespace ArtistApi.Services
{
    public interface IArtistService
    {
        /// <summary>
        /// Gets all Artists
        /// </summary>
        /// <returns>Returns all Artists as ArtistDto</returns>
        Task<IEnumerable<ArtistDto>> GetAllArtists();

        /// <summary>
        /// Gets all artists from a given <paramref name="songId"/>
        /// </summary>
        /// <param name="songId"></param>
        /// <returns>Returns all Artists from a movie as ArtistDto</returns>
        Task<IEnumerable<ArtistDto>> GetAllArtists(int songId);

        /// <summary>
        /// Gets a single Artist from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns an artist as ArtistDto</returns>
        Task<ArtistDto?> GetArtistById(int id);

        /// <summary>
        /// Creates a new Artist from <paramref name="artistToCreate"/>
        /// </summary>
        /// <param name="artistToCreate"></param>
        /// <returns>Returns the newly created Artist as ArtistDto</returns>
        Task<ArtistDto> CreateArtist(ArtistCreationDto artistToCreate);

        /// <summary>
        /// Updates an existing artist record from <paramref name="id"/> given data <paramref name="artistToUpdate"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="artistToUpdate"></param>
        /// <returns>Returns true if update is successful, false otherwise</returns>
        Task<bool> UpdateArtist(int id, ArtistUpdateDto artistToUpdate);

        /// <summary>
        /// Deletes an artist from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true if delete is successful, false otherwise</returns>
        Task<bool> DeleteArtist(int id);
    }
}
