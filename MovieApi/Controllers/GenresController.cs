using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieApi.Dtos.Genre;
using MovieApi.Dtos.Movie;
using MovieApi.Services;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly ISongService _songService;
        private readonly ISongGenreService _songGenreService;
        private readonly ILogger<GenresController> _logger;

        public GenresController(IGenreService genreService, ILogger<GenresController> logger, ISongService songService, ISongGenreService songGenreService)
        {
            _genreService = genreService;
            _logger = logger;
            _songService = songService;
            _songGenreService = songGenreService;
        }

        /// <summary>
        /// Gets all genres
        /// </summary>
        /// <returns>Returns all genres</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Genres
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned genres</response>
        /// <response code = "204">Genres have no content</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet(Name = "GetAllGenres")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllGenres()
        {
            try
            {
                var genres = await _genreService.GetAllGenres();

                if (genres.IsNullOrEmpty())
                {
                    return NoContent();
                }
                return Ok(genres);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets a single genre by a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a single genre</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Genres/1
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned genre</response>
        /// <response code = "404">Genre with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet("{id}", Name = "GetGenreById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGenreById(int id)
        {
            try
            {
                var genre = await _genreService.GetGenreById(id);

                if (genre == null)
                {
                    return NotFound($"Genre with id {id} does not exist");
                }

                return Ok(genre);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all songs of a genre
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns all songs of a genre</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Genres/1/songs
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned songs</response>
        /// <response code = "204">Genre have no songs</response>
        /// <response code = "404">Genre with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet("{id}/songs", Name = "GetGenreSongs")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SongDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGenreSongs(int id)
        {
            try
            {
                var genre = await _genreService.GetGenreById(id);

                if (genre == null)
                {
                    return NotFound($"Genre with id {id} does not exist");
                }

                var songs = await _songService.GetAllSongsByGenreId(id);
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
        /// Creates a genre
        /// </summary>
        /// <param name="genreToCreate">Genre details</param>
        /// <returns>Returns the newly created genre</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Genres
        ///     {
        ///         "name" : "Comedy"
        ///     }
        /// 
        /// </remarks>
        /// <response code = "201">Successfully created a genre</response>
        /// <response code = "400">Genre details are invalid / Genre already existing</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPost(Name = "CreateGenre")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GenreDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateGenre([FromBody] GenreCreationDto genreToCreate)
        {
            try
            {
                //check if genre already exists
                var genreCheck = await _genreService.GetGenreByName(genreToCreate.Name!);
                if (genreCheck != null) return BadRequest("Genre already exists");

                var newGenre = await _genreService.CreateGenre(genreToCreate);

                return CreatedAtRoute("GetGenreById", new { id = newGenre.Id }, newGenre);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Adds a songs to a genre
        /// </summary>
        /// <param name="songId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Genres/1/songs/2
        /// 
        /// </remarks>
        /// <response code = "200">Successfully added songs to genre</response>
        /// <response code = "400">Song is already in genre</response>
        /// <response code = "404">Genre / Song with the given ids does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPut("{id}/songs/{songId}", Name = "AddSongToGenre")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddSongToGenre(int songId, int id)
        {
            try
            {
                var genre = await _genreService.GetGenreById(id);

                if (genre == null)
                {
                    return NotFound($"Genre with id {id} does not exist");
                }

                var song = await _songService.GetSongOnly(songId);

                if (song == null)
                {
                    return NotFound($"Song with id {songId} does not exist");
                }

                //check if genre is already in song
                if (await _songGenreService.IsGenreInSong(songId, id))
                {
                    return BadRequest("Song Already In Genre");
                }

                var isComplete = await _songGenreService.AddGenreInSong(songId, id);
                return Ok("Added Genre to Song");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates an existing genre
        /// </summary>
        /// <param name="id"></param>
        /// <param name="genreToUpdate"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Genres/1
        ///     {
        ///         "name" : "Action"
        ///     }
        /// 
        /// </remarks>
        /// <response code = "200">Successfully updated genre</response>
        /// <response code = "400">Genre details are invalid / Genre already existing</response>
        /// <response code = "404">Genre with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPut("{id}", Name = "UpdateGenre")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreUpdateDto genreToUpdate)
        {
            try
            {
                //check if genre exists
                var checkGenre = await _genreService.GetGenreById(id);
                if (checkGenre == null)
                {
                    return NotFound($"Genre with id {id} does not exist");
                }

                //check if genre already exists
                var genreCheck = await _genreService.GetGenreByName(genreToUpdate.Name!);
                if (genreCheck != null) return BadRequest("Genre already exists");

                var isGenreUpdated = await _genreService.UpdateGenre(id, genreToUpdate);
                return Ok("Genre Updated");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes a genre
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Genres/1
        /// 
        /// </remarks>
        /// <response code = "200">Successfully deleted a genre</response>
        /// <response code = "404">Genre with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpDelete("{id}", Name = "DeleteGenre")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            try
            {
                var checkGenre = await _genreService.GetGenreById(id);
                if (checkGenre == null)
                {
                    return NotFound($"Genre with id {id} does not exist");
                }

                var isGenreDeleted = await _genreService.DeleteGenre(id);
                return Ok("Genre Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Removes a songs from a genre
        /// </summary>
        /// <param name="id"></param>
        /// <param name="songId"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Genres/1/songs/2
        /// 
        /// </remarks>
        /// <response code = "200">Successfully deleted song from genre</response>
        /// <response code = "400">Song is not in genre</response>
        /// <response code = "404">Genre / Song with the given ids does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpDelete("{id}/songs/{songId}", Name = "DeleteSongFromGenre")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSongFromGenre(int id, int songId)
        {
            try
            {
                var genre = await _genreService.GetGenreById(id);

                if (genre == null)
                {
                    return NotFound($"Genre with id {id} does not exist");
                }

                var song = await _songService.GetSongOnly(songId);

                if (song == null)
                {
                    return NotFound($"Song with id {songId} does not exist");
                }

                if (!(await _songGenreService.IsGenreInSong(songId, id)))
                {
                    return BadRequest("Song is NOT in Genre");
                }

                var isSongFromGenreDeleted = await _songGenreService.DeleteGenreInSong(songId, id);
                return Ok("Song From Genre Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
