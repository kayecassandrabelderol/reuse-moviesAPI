using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieApi.Dtos.Actor;
using MovieApi.Dtos.Movie;
using MovieApi.Services;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly ISongService _songService;
        private readonly ISongArtistService _songArtistService;
        private readonly ILogger<ArtistsController> _logger;

        public ArtistsController(IArtistService artistService, ILogger<ArtistsController> logger, ISongService songService, ISongArtistService songArtistService)
        {
            _artistService = artistService;
            _logger = logger;
            _songService = songService;
            _songArtistService = songArtistService;
        }

        /// <summary>
        /// Gets all artists
        /// </summary>
        /// <returns>Returns all artists</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Artists
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned artists</response>
        /// <response code = "204">Artists have no content</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet(Name = "GetAllArtists")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ArtistDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllArtists()
        {
            try
            {
                var artists = await _artistService.GetAllArtists();
                if (artists.IsNullOrEmpty())
                {
                    return NoContent();
                }
                return Ok(artists);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets a single artist by a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a single artist</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Artists/1
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned artist</response>
        /// <response code = "404">Artist with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet("{id}", Name = "GetArtistById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ArtistDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetArtistById(int id)
        {
            try
            {
                var artist = await _artistService.GetArtistById(id);

                if (artist == null)
                {
                    return NotFound($"Artist with id {id} does not exist");
                }

                return Ok(artist);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all songs of an artist
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns all songs of an artist</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Artists/1/songs
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned songs</response>
        /// <response code = "204">Artist have no songs</response>
        /// <response code = "404">Artist with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet("{id}/songs", Name = "GetArtistSongs")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SongDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetArtistSongs(int id)
        {
            try
            {
                var artist = await _artistService.GetArtistById(id);

                if (artist == null)
                {
                    return NotFound($"Artist with id {id} does not exist");
                }

                var songs = await _songService.GetAllSongsByArtistId(id);
                if (!songs.Any())
                {
                    return NoContent();
                }

                return Ok(songs);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Creates an artist
        /// </summary>
        /// <param name="artistToCreate">Artist details</param>
        /// <returns>Returns the newly created artist</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Artists
        ///     {
        ///         "name" : "Will Smith",
        ///         "gender" : "Male",
        ///         "birthday" : "9-25-1968"
        ///     }
        /// 
        /// </remarks>
        /// <response code = "201">Successfully created an artist</response>
        /// <response code = "400">Artist details are invalid</response>
        /// <response code = "500">Internal Server Error</response>

        [HttpPost(Name = "CreateArtist")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ArtistDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateArtist([FromBody] ArtistCreationDto artistToCreate)
        {
            try
            {              
                var newArtist = await _artistService.CreateArtist(artistToCreate);

                return CreatedAtRoute("GetArtistById", new { id = newArtist.Id }, newArtist);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Adds a song to an artist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="songId"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Artists/1/songs/2
        /// 
        /// </remarks>
        /// <response code = "200">Successfully added song to artist</response>
        /// <response code = "400">Song is already in artist</response>
        /// <response code = "404">Artist / Song with the given ids does not exist</response>
        /// <response code = "500">Internal Server Error</response>

        [HttpPut("{id}/songs/{songId}", Name = "AddSongToArtist")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddSongToArtist(int id, int songId)
        {
            try
            {
                var artist = await _artistService.GetArtistById(id);

                if (artist == null)
                {
                    return NotFound($"Artist with id {id} does not exist");
                }

                var song = await _songService.GetSongOnly(songId);

                if (song == null)
                {
                    return NotFound($"Song with id {songId} does not exist");
                }                

                //check if song is already in artist
                if (await _songArtistService.IsArtistInSong(songId, id) == true)
                {
                    return BadRequest("Song Already In Artist");
                }

                var isComplete = await _songArtistService.AddArtistInSong(songId, id);
                return Ok("Added Song in Artist");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates an existing artist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="artistToUpdate"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request
        /// 
        ///     PUT /api/Artists/1
        ///     {
        ///         "name" : "William Smithereen"
        ///     }
        /// 
        /// </remarks>
        /// <response code = "200">Successfully updated artist</response>
        /// <response code = "400">Artist details are invalid</response>
        /// <response code = "404">Artist with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPut("{id}", Name = "UpdateArtist")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateArtist(int id, [FromBody] ArtistUpdateDto artistToUpdate)
        {
            try
            {
                //check if artist exists
                var checkArtist = await _artistService.GetArtistById(id);
                if (checkArtist == null)
                {
                    return NotFound($"Artist with id {id} does not exist");
                }

                var isArtistUpdated = await _artistService.UpdateArtist(id, artistToUpdate);
                return Ok("Artist Updated");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an artist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Artists/1
        /// 
        /// </remarks>
        /// <response code = "200">Successfully deleted an artist</response>
        /// <response code = "404">Artist with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpDelete("{id}", Name = "DeleteArtist")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            try
            {
                var checkArtist = await _artistService.GetArtistById(id);
                if (checkArtist == null)
                {
                    return NotFound($"Artist with id {id} does not exist");
                }

                var isArtistDeleted = await _artistService.DeleteArtist(id);
                return Ok("Artist Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Removes a song from an artist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="songId"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Artists/1/songs/2
        /// 
        /// </remarks>
        /// <response code = "200">Successfully deleted song from artist</response>
        /// <response code = "400">Sopng is not in artist</response>
        /// <response code = "404">Artist / Song with the given ids does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpDelete("{id}/songs/{songId}", Name = "DeleteSongFromArtist")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSongFromArtist(int id, int songId)
        {
            try
            {
                var artist = await _artistService.GetArtistById(id);

                if (artist == null)
                {
                    return NotFound($"Artist with id {id} does not exist");
                }

                var song = await _songService.GetSongOnly(songId);

                if (song == null)
                {
                    return NotFound($"Song with id {songId} does not exist");
                }

                if (!(await _songArtistService.IsArtistInSong(songId, id)))
                {
                    return BadRequest("Song is NOT in Artist");
                }

                var isSongFromArtistDeleted = await _songArtistService.DeleteArtistInSong(songId, id);
                return Ok("Song From Artist Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
