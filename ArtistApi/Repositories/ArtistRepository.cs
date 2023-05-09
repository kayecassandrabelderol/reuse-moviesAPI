using ArtistApi.Context;
using ArtistApi.Contracts;
using ArtistApi.Models;
using Dapper;
using System.Data;

namespace ArtistApi.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly DapperContext _context;

        public ArtistRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Artist artist)
        {
            var sql = @"INSERT INTO Artist (Name, Gender, Birthday) VALUES (@Name, @Gender, @Birthday);
                        SELECT SCOPE_IDENTITY();";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(sql, artist);
            }
        }

        public async Task<IEnumerable<Artist>> GetAll()
        {
            var sql = "SELECT * FROM Artist";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Artist>(sql);
            }
        }

        public async Task<Artist?> GetArtist(int id)
        {
            var sql = @"SELECT * 
                        FROM Artist ac 
                        WHERE ac.Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Artist>(sql, new { id });
            }
        }

        public async Task<IEnumerable<Artist>> GetAllBySongId(int songId)
        {
            var sql = "spArtist_GetAllBySongId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Artist>(sql, new { songId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> Update(Artist artist)
        {
            var sql = @"UPDATE Artist
                        SET Name = @Name 
                        WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var update = await connection.ExecuteAsync(sql, artist);
                return update == 1;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var sql = "DELETE FROM Artist WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var delete = await connection.ExecuteAsync(sql, new { id });
                return delete == 1;
            }
        }
    }
}
