namespace Common.HttpCLients;

public interface IEmployeeHttpClient
{
    public Task<string> GetEmployeeByAccountIdAsync(Guid employeeAccountId,  CancellationToken cancellationToken = default(CancellationToken));
    public Task<string> GetEmployeeByIdAsync(Guid employeeId, CancellationToken cancellationToken = default(CancellationToken));
    public Task<string> GetAllDevelopersAsync(CancellationToken cancellationToken = default(CancellationToken));
}