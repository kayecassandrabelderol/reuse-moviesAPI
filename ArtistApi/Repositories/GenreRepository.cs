using Dapper;
using ArtistApi.Context;
using ArtistApi.Contracts;
using ArtistApi.Models;
using System.Data;

namespace ArtistApi.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DapperContext _context;
        public GenreRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Genre genre)
        {
            var sql = @"INSERT INTO Genre (Name) VALUES (@Name);
                        SELECT SCOPE_IDENTITY();";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(sql, genre);
            }
        }

        public async Task<bool> Delete(int id)
        {
            var sql = "DELETE FROM Genre WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var delete = await connection.ExecuteAsync(sql, new { id });
                return delete == 1;
            }
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            var sql = "SELECT * FROM Genre";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Genre>(sql);
            }
        }

        public async Task<IEnumerable<Genre>> GetAllBySongId(int songId)
        {
            var sql = "spGenre_GetAllBySongId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Genre>(sql, new { songId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Genre?> GetGenre(int id)
        {
            var sql = @"SELECT *
                        FROM Genre g
                        WHERE g.Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Genre>(sql, new { id });
            }
        }

        public async Task<Genre?> GetGenre(string name)
        {
            var sql = @"SELECT *
                        FROM Genre g
                        WHERE g.Name = @Name";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Genre>(sql, new { name });
            }
        }

        public async Task<bool> Update(Genre genre)
        {
            var sql = @"UPDATE Genre
                        SET Name = @Name 
                        WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var update = await connection.ExecuteAsync(sql, genre);
                return update == 1;
            }
        }
    }

}
