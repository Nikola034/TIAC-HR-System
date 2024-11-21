namespace Common.HttpCLients;

public interface IEmployeeHttpClient
{
    public Task<string> GetEmployeeByAccountIdAsync(Guid employeeAccountId, CancellationToken cancellationToken = default(CancellationToken));
    public Task<string> GetEmployeeByIdAsync(Guid employeeId, string token, CancellationToken cancellationToken = default(CancellationToken));
    public Task<string> GetAllDevelopersAsync(string token, CancellationToken cancellationToken = default(CancellationToken));
}