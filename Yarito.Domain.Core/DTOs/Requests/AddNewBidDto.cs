namespace Yarito.Domain.Core.DTOs.Requests;

public class AddNewBidDto
{
    public int RequestId { get; set; }
    public int ExpertId { get; set; }
    public decimal ProposedPrice { get; set; }
    public DateTime ProposedVisitDateTime { get; set; }
    public string? Description { get; set; }
}
