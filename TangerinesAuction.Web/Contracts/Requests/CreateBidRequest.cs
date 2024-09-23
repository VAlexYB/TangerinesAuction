namespace TangerinesAuction.Web.Contracts.Requests
{
    public record CreateBidRequest
    (
        Guid TangerineId,
        decimal Amount
    );
}
