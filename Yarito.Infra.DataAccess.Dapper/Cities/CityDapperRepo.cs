using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Yarito.Domain.Core.Contracts.Cities.Repository;
using Yarito.Domain.Core.DTOs.Cities;

namespace Yarito.Infra.DataAccess.Dapper.Cities;

public class CityQueryRepo(IConfiguration configuration) : ICityQueryRepo
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                                                ?? throw new ArgumentNullException(nameof(configuration), "DefaultConnection is not configured");

    public async Task<IReadOnlyList<CityFullDto>> GetAllAsync(CancellationToken ct)
    {
        const string sql = @"
            SELECT 
                c.Id, 
                c.Name, 
                p.Name AS ParentName
            FROM Cities c
            INNER JOIN Cities p ON c.ParentId = p.Id
            WHERE c.IsDeleted = 0 AND c.ParentId IS NOT NULL
            ORDER BY c.Id";

        await using var connection = new SqlConnection(_connectionString);
        var cities = await connection.QueryAsync<CityFullDto>(sql);
        return cities.AsList();
    }
}
