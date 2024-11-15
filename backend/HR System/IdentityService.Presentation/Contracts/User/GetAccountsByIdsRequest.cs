namespace IdentityService.Presentation.Contracts.User
{
    public class GetAccountsByIdsRequest
    {
        public IEnumerable<string> Ids { get; set; }
    }
}
