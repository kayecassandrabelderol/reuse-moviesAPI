using Dapper;
using ArtistApi.Context;
using ArtistApi.Contracts;
using ArtistApi.Models;
using System.Data;

namespace ArtistApi.Repositories
{
    public class AwardRepository : IAwardRepository
    {
        private readonly DapperContext _context;

        public AwardRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Award award)
        {
            var sql = @"INSERT INTO Award (Name, Year, SongId) VALUES (@Name, @Year, @SongId);
                        SELECT SCOPE_IDENTITY();";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(sql, award);
            }
        }

        public async Task<bool> Delete(int id)
        {
            var sql = "DELETE FROM Award WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var delete = await connection.ExecuteAsync(sql, new { id });
                return delete == 1;
            }
        }

        public async Task<IEnumerable<Award>> GetAll()
        {
            var sql = "SELECT * FROM Award";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Award>(sql);
            }
        }

        public async Task<IEnumerable<Award>> GetAllBySongId(int songId)
        {
            var sql = "spAward_GetAllBySongId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Award>(sql, new { songId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Award?> GetAward(int id)
        {
            var sql = @"SELECT *
                        FROM Award aw
                        WHERE aw.Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Award>(sql, new { id });
            }
        }

        public async Task<bool> Update(Award award)
        {
            var sql = @"UPDATE Award
                        SET Name = @Name, Year = @Year
                        WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var update = await connection.ExecuteAsync(sql, award);
                return update == 1;
            }
        }
    }
}
