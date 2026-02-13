using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Yarito.Domain.Core.Contracts.Works.Repository;
using Yarito.Domain.Core.DTOs.Works;

namespace Yarito.Infra.DataAccess.Dapper.Works;

public class WorkQueryRepo(IConfiguration configuration) : IWorkQueryRepo
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                                                ?? throw new ArgumentNullException(nameof(configuration), "DefaultConnection is not configured");

    public async Task<List<WorksFullDto>> GetWorksByIDs(List<int> ids, CancellationToken ct)
    {
        if (ids == null || ids.Count == 0)
            return new List<WorksFullDto>();

        const string sql = @"
            SELECT Id, Title, BasePrice
            FROM Works
            WHERE Id IN @Ids AND IsDeleted = 0";

        await using var connection = new SqlConnection(_connectionString);
        var works = await connection.QueryAsync<WorksFullDto>(sql, new { Ids = ids });
        return works.ToList();
    }
}
