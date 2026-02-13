using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Yarito.Domain.Core.Contracts.Works.Repository;
using Yarito.Domain.Core.DTOs.Works;

namespace Yarito.Infra.DataAccess.Dapper.Works;

public class CategoryQueryRepo(IConfiguration configuration) : ICategoryQueryRepo
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                                                ?? throw new ArgumentNullException(nameof(configuration), "DefaultConnection is not configured");

    public async Task<IReadOnlyList<CategoryStringDataDto>> GetAllCategoriesNamesAsync(CancellationToken ct)
    {
        const string sql = @"
            SELECT 
                c.Title AS CategoryTitle, 
                c.Description,
                w.Title AS WorkTitle
            FROM Categories c
            LEFT JOIN Works w ON w.CategoryId = c.Id AND w.IsDeleted = 0
            WHERE c.IsDeleted = 0
            ORDER BY c.Id";

        await using var connection = new SqlConnection(_connectionString);
        
        var rows = await connection.QueryAsync(sql);
        
        var categories = rows
            .GroupBy(row => new 
            { 
                Title = (string)row.CategoryTitle, 
                Description = (string?)row.Description 
            })
            .Select(group => new CategoryStringDataDto
            {
                Title = group.Key.Title,
                Description = group.Key.Description,
                WorksTitle = group
                    .Select(row => (string?)row.WorkTitle)
                    .Where(title => !string.IsNullOrEmpty(title))
                    .ToList()
            })
            .ToList();

        return categories;
    }

    public async Task<IReadOnlyList<CategoryDto>> GetJustCategoriesListAsync(CancellationToken ct)
    {
        const string sql = """
                           SELECT Id, Title FROM Categories WHERE IsDeleted = 0 ORDER BY Title 
                           """;

        await using var connection = new SqlConnection(_connectionString);
        var categories = await connection.QueryAsync<CategoryDto>(sql);
        return categories.AsList();
    }
}
