using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.DTOs.Requests;
public class RequestCardDto
{
    public required int Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required DateTime CreatedAt { get; init; }
    public int BidCount { get; set; } = 0;
    public string? CustomerProfileImagePath { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required RequestStatusEnum Status { get; init; }
    public string? CityName { get; init; }
}