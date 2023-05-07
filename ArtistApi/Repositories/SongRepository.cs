using Dapper;
using MovieApi.Context;
using MovieApi.Contracts;
using MovieApi.Models;
using System.Data;

namespace MovieApi.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly DapperContext _context;
        public SongRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Song song)
        {
            var sql = @"INSERT INTO Song (Title, Duration, ReleaseDate) VALUES (@Title, @Duration, @ReleaseDate);
                        SELECT SCOPE_IDENTITY();";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(sql, song);
            }
        }

        public async Task<IEnumerable<Song>> GetAll()
        {
            var sql = "SELECT * FROM Song";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Song>(sql);
            }
        }

        public async Task<IEnumerable<Song>> GetAllByArtistId(int artistId)
        {
            var sql = "spSong_GetAllByArtistId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Song>(sql, new { artistId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Song>> GetAllByGenreId(int genreId)
        {
            var sql = "spSong_GetAllByGenreId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Song>(sql, new { genreId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Song?> GetSong(int id)
        {
            var sql = "spSong_GetSongDetails";

            using (var connection = _context.CreateConnection())
            {
                var song = await connection.QueryAsync<Song, Genre, Artist, Award, Song>(
                    sql,
                    map: (song, genre, artist, award) =>
                    {
                        song.Genres.Add(genre);
                        song.Artists.Add(artist);
                        song.Awards.Add(award);
                        return song;
                    },
                    param: new { id },
                    commandType: CommandType.StoredProcedure);

                var songerist = song.GroupBy(m => m.Id).Select(mg =>
                {
                    var firstsong = mg.First();
                    firstsong.Genres = mg.SelectMany(a => a.Genres).ToList();
                    firstsong.Artists = mg.SelectMany(a => a.Artists).ToList();
                    firstsong.Awards = mg.SelectMany(a => a.Awards).ToList();
                    return firstsong;
                }).First();

                return songerist;
            }
        }
        public async Task<Song?> GetSongOnly(int id)
        {
            var sql = @"SELECT *
                        FROM Song m
                        WHERE m.Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Song?>(sql, new { id });
            }
        }

        public async Task<bool> IsArtistInSong(int songId, int artistId)
        {
            var sql = "spSongArtist_IsArtistInSong";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<bool>(sql, new { songId, artistId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> AddArtistInSong(int songId, int artistId)
        {
            var sql = @"INSERT INTO SongArtist (SongId, ArtistId) VALUES (@songId, @artistId)";

            using (var connection = _context.CreateConnection())
            {
                var res = await connection.ExecuteAsync(sql, new { songId, artistId });
                return res == 1;
            }
        }

        public async Task<bool> IsGenreInSong(int songId, int genreId)
        {
            var sql = @"spSongGenre_IsGenreInSong";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<bool>(sql, new { songId, genreId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> AddGenreInSong(int songId, int genreId)
        {
            var sql = @"INSERT INTO SongGenre (SongId, GenreId) VALUES (@songId, @genreId)";

            using (var connection = _context.CreateConnection())
            {
                var res = await connection.ExecuteAsync(sql, new { songId, genreId });
                return res == 1;
            }
        }

        public async Task<bool> Update(Song song)
        {
            var sql = @"UPDATE Song
                        SET ReleaseDate = @ReleaseDate
                        WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var update = await connection.ExecuteAsync(sql, song);
                return update == 1;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var sql = "DELETE FROM Song WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var delete = await connection.ExecuteAsync(sql, new { id });
                return delete == 1;
            }
        }

        public async Task<bool> DeleteGenreInSong(int songId, int genreId)
        {
            var sql = @"DELETE FROM SongGenre
                        WHERE SongId = @songId AND GenreId = @genreId";

            using (var connection = _context.CreateConnection())
            {
                var delete = await connection.ExecuteAsync(sql, new { songId, genreId });
                return delete == 1;
            }
        }

        public async Task<bool> DeleteArtistInSong(int songId, int artistId)
        {
            var sql = @"DELETE FROM SongArtist
                        WHERE SongId = @songId AND ArtistId = @artistId";

            using (var connection = _context.CreateConnection())
            {
                var delete = await connection.ExecuteAsync(sql, new { songId, artistId });
                return delete == 1;
            }
        }
    }
}
