namespace IdentityService.Presentation.Contracts.User
{
    public class GetAccountsByIdsResponse
    {
        public IEnumerable<Core.Entities.Account> Accounts { get; set;  }
    }
}
