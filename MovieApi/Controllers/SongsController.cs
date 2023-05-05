using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieApi.Dtos.Actor;
using MovieApi.Dtos.Award;
using MovieApi.Dtos.Genre;
using MovieApi.Dtos.Movie;
using MovieApi.Services;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ISongService _songService;
        private readonly IGenreService _genreService;
        private readonly IActorService _actorService;
        private readonly IAwardService _awardService;
        private readonly ISongArtistService _songArtistService;
        private readonly ISongGenreService _songGenreService;
        private readonly ILogger<SongsController> _logger;

        public SongsController(
            ISongService songService,
            ILogger<SongsController> logger,
            IGenreService genreService,
            IActorService actorService,
            IAwardService awardService,
            ISongArtistService songArtistService,
            ISongGenreService songGenreService)
        {
            _songService = songService;
            _logger = logger;
            _genreService = genreService;
            _actorService = actorService;
            _awardService = awardService;
            _songArtistService = songArtistService;
            _songGenreService = songGenreService;
        }

        /// <summary>
        /// Gets all songs
        /// </summary>
        /// <returns>Returns all songs</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Songs
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned songs</response>
        /// <response code = "204">Songs have no content</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet(Name = "GetAllSongs")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SongDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSongs()
        {
            try
            {
                var songs = await _songService.GetAllSongs();

                if (songs.IsNullOrEmpty())
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
        /// Gets a single song by a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a single song</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Songs/1
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned song</response>
        /// <response code = "404">Song with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet("{id}", Name = "GetSongById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SongByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMovieById(int id)
        {
            try
            {
                var songCheck = await _songService.GetSongOnly(id);

                if (songCheck == null)
                {
                    return NotFound($"Song with id {id} does not exist");
                }

                var song = await _songService.GetSongById(id);

                return Ok(song);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all genres of a song
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns all genres of a song</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Songs/1/genres
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned genres</response>
        /// <response code = "204">Song have no genres</response>
        /// <response code = "404">Song with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet("{id}/genres", Name = "GetSongGenres")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSongGenres(int id)
        {
            try
            {
                var song = await _songService.GetSongOnly(id);

                if (song == null)
                {
                    return NotFound($"Song with id {id} does not exist");
                }

                var genres = await _genreService.GetAllGenres(id);
                if (!genres.Any())
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
        /// Gets all artist of a song
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns all artist of a song</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Songs/1/artists
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned artist</response>
        /// <response code = "204">Song have no artists</response>
        /// <response code = "404">Song with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet("{id}/artists", Name = "GetSongArtists")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ActorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSongArtists(int id)
        {
            try
            {
                var song = await _songService.GetSongOnly(id);

                if (song == null)
                {
                    return NotFound($"Song with id {id} does not exist");
                }

                var actors = await _actorService.GetAllActors(id);
                if (!actors.Any())
                {
                    return NoContent();
                }

                return Ok(actors);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all awards of a song
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns all awards of a song</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Songs/1/awards
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned awards</response>
        /// <response code = "204">Song have no awards</response>
        /// <response code = "404">Song with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet("{id}/awards", Name = "GetSongAwards")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AwardDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMovieAwards(int id)
        {
            try
            {
                var song = await _songService.GetSongOnly(id);

                if (song == null)
                {
                    return NotFound($"Song with id {id} does not exist");
                }

                var awards = await _awardService.GetAllAwards(id);
                if (!awards.Any())
                {
                    return NoContent();
                }

                return Ok(awards);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Creates a song
        /// </summary>
        /// <param name="songToCreate">Song details</param>
        /// <returns>Returns the newly created song</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Songs
        ///     {
        ///         "title" : "Kill This Love",
        ///         "duration" : 3,
        ///         "releaseDate" : "04-04-2019"
        /// 
        /// </remarks>
        [HttpPost(Name = "CreateSong")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SongDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSong([FromBody] SongCreationDto songToCreate)
        {
            try
            {
                var newSong = await _songService.CreateSong(songToCreate);

                return CreatedAtRoute("GetSongById", new { id = newSong.Id }, newSong);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Adds an artist to song
        /// </summary>
        /// <param name="id"></param>
        /// <param name="artistId"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Songs/1/artists/2
        /// 
        /// </remarks>
        /// <response code = "200">Successfully added artist to song</response>
        /// <response code = "400">Song is already in movie</response>
        /// <response code = "404">Song / Artist with the given ids does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPut("{id}/artists/{artistId}", Name = "AddArtistToSong")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddActorToMovie(int id, int artistId)
        {
            try
            {
                var song = await _songService.GetSongOnly(id);

                if (song == null)
                {
                    return NotFound($"Song with id {id} does not exist");
                }

                var artist = await _actorService.GetActorById(artistId);

                if (artist == null)
                {
                    return NotFound($"Artist with id {artistId} does not exist");
                }

                //check if actor is already in movie
                if (await _songArtistService.IsArtistInSong(id, artistId) == true)
                {
                    return BadRequest("Artist Already In Song");
                }

                var isComplete = await _songArtistService.AddArtistInSong(id, artistId);
                return Ok("Added Artist to Song");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Adds an genre to song
        /// </summary>
        /// <param name="id"></param>
        /// <param name="genreId"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Songs/1/genres/2
        /// 
        /// </remarks>
        /// <response code = "200">Successfully added genre to song</response>
        /// <response code = "400">Genre is already in song</response>
        /// <response code = "404">Song / Artist with the given ids does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPut("{id}/genres/{genreId}", Name = "AddGenreToSong")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddGenreToSong(int id, int genreId)
        {
            try
            {
                var song = await _songService.GetSongOnly(id);

                if (song == null)
                {
                    return NotFound($"Song with id {id} does not exist");
                }

                var genre = await _genreService.GetGenreById(genreId);

                if (genre == null)
                {
                    return NotFound($"Genre with id {genreId} does not exist");
                }

                //check if genre is already in movie
                if (await _songGenreService.IsGenreInSong(id, genreId))
                {
                    return BadRequest("Genre Already In Song");
                }

                var isComplete = await _songGenreService.AddGenreInSong(id, genreId);
                return Ok("Added Genre to Song");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates an existing song
        /// </summary>
        /// <param name="id"></param>
        /// <param name="songToUpdate"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Songs/1
        ///     {
        ///         "releaseDate" : "9-11-2002"
        ///     }
        /// 
        /// </remarks>
        /// <response code = "200">Successfully updated song</response>
        /// <response code = "400">Song details are invalid</response>
        /// <response code = "404">Song with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPut("{id}", Name = "UpdateSong")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSong(int id, [FromBody] SongUpdateDto songToUpdate)
        {
            try
            {
                //check if movie exists
                var checkSong = await _songService.GetSongOnly(id);
                if (checkSong == null)
                {
                    return NotFound($"Song with id {id} does not exist");
                }

                var isSongUpdated = await _songService.UpdateSong(id, songToUpdate);
                return Ok("Song Updated");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes a song
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Songs/1
        /// 
        /// </remarks>
        /// <response code = "200">Successfully deleted a song</response>
        /// <response code = "404">Song with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpDelete("{id}", Name = "DeleteSong")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSong(int id)
        {
            try
            {
                var checkSong = await _songService.GetSongOnly(id);
                if (checkSong == null)
                {
                    return NotFound($"Movie with id {id} does not exist");
                }

                var isSongDeleted = await _songService.DeleteSong(id);
                return Ok("Song Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Removes a genre from song
        /// </summary>
        /// <param name="id"></param>
        /// <param name="genreId"></param>
        /// <returns></returns>
        /// <remarks>Sample request:
        /// 
        ///     DELETE /api/Songs/1/genres/2
        /// 
        /// </remarks>
        /// <response code = "200">Successfully deleted genre from song</response>
        /// <response code = "400">Genre is not in song</response>
        /// <response code = "404">Song / Genre with the given ids does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpDelete("{id}/genres/{genreId}", Name = "DeleteGenreFromSong")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteGenreFromSong(int id, int genreId)
        {
            try
            {
                var song = await _songService.GetSongOnly(id);

                if (song == null)
                {
                    return NotFound($"Song with id {id} does not exist");
                }

                var genre = await _genreService.GetGenreById(genreId);

                if (genre == null)
                {
                    return NotFound($"Genre with id {genreId} does not exist");
                }

                if (!(await _songGenreService.IsGenreInSong(id, genreId)))
                {
                    return BadRequest("Genre is NOT in Song");
                }

                var isGenreFromMovieDeleted = await _songGenreService.DeleteGenreInSong(id, genreId);
                return Ok("Genre From Song Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Removes an actor from song
        /// </summary>
        /// <param name="id"></param>
        /// <param name="artistId"></param>
        /// <returns></returns>
        /// <remarks>Sample request:
        /// 
        ///     DELETE /api/Songs/1/artists/2
        /// 
        /// </remarks>
        /// <response code = "200">Successfully deleted artist from song</response>
        /// <response code = "400">Artist is not in movie</response>
        /// <response code = "404">Song / Artist with the given ids does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpDelete("{id}/artists/{artistId}", Name = "DeleteArtistFromSong")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteActorFromMovie(int id, int artistId)
        {
            try
            {
                var song = await _songService.GetSongOnly(id);

                if (song == null)
                {
                    return NotFound($"Song with id {id} does not exist");
                }

                var actor = await _actorService.GetActorById(artistId);

                if (actor == null)
                {
                    return NotFound($"Artist with id {artistId} does not exist");
                }

                if (!(await _songArtistService.IsArtistInSong(id, artistId)))
                {
                    return BadRequest("Artist is NOT in Song");
                }

                var isActorFromMovieDeleted = await _songArtistService.DeleteArtistInSong(id, artistId);
                return Ok("Artist From Song is Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
