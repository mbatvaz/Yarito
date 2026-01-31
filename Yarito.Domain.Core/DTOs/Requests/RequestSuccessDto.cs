using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.DTOs.Requests
{
    public class RequestSuccessDto
    {
        public required string Title { get; init; }
        public required string WorkTitle { get; init; }
        public required DateTime Date { get; init; }
        public required string Address { get; init; }
        public required RequestStatusEnum Status { get; init; }
    }
}
