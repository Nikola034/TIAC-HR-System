using Application.Common.Services;


namespace Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public Task<string> HashPasswordAsync(string plainTextPassword, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(BCrypt.Net.BCrypt.HashPassword(plainTextPassword));
        }

        public Task<bool> VerifyPasswordAsync(string plainTextPassword, string password, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(BCrypt.Net.BCrypt.Verify(plainTextPassword, password));
        }
    }
}
