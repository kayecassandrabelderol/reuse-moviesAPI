using AutoMapper;
using ArtistApi.Contracts;
using ArtistApi.Dtos.Actor;
using ArtistApi.Models;

namespace ArtistApi.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;

        public ArtistService(IArtistRepository artistRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
        }

        public async Task<ArtistDto?> GetArtistById(int id)
        {
            var artistModel = await _artistRepository.GetArtist(id);
            if (artistModel == null) return null;

            return _mapper.Map<ArtistDto>(artistModel);
        }

        public async Task<IEnumerable<ArtistDto>> GetAllArtists()
        {
            var artistModels = await _artistRepository.GetAll();

            return _mapper.Map<IEnumerable<ArtistDto>>(artistModels);
        }

        public async Task<ArtistDto> CreateArtist(ArtistCreationDto artistToCreate)
        {
            var artistModel = _mapper.Map<Artist>(artistToCreate);

            artistModel.Id = await _artistRepository.Create(artistModel);

            return _mapper.Map<ArtistDto>(artistModel);
        }

        public async Task<IEnumerable<ArtistDto>> GetAllArtists(int songId)
        {
            var artistModels = await _artistRepository.GetAllBySongId(songId);

            return _mapper.Map<IEnumerable<ArtistDto>>(artistModels);
        }

        public async Task<bool> UpdateArtist(int id, ArtistUpdateDto artistToUpdate)
        {
            var artistModel = _mapper.Map<Artist>(artistToUpdate);
            artistModel.Id = id;

            return await _artistRepository.Update(artistModel);
        }

        public async Task<bool> DeleteArtist(int id)
        {
            return await _artistRepository.Delete(id);
        }
    }
}
