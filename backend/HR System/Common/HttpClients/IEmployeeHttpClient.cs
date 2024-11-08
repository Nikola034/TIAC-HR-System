namespace Common.HttpCLients;

public interface IEmployeeHttpClient
{
    public Task<string> GetEmployeeRole(Guid employeeAccountId,  CancellationToken cancellationToken = default(CancellationToken));
}