using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Application.Common.Repositories
{
    public interface IAccountRepository
    {
        public Task<Account?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken));
        public Task<Account> CreateAsync(Account account, CancellationToken cancellationToken = default(CancellationToken));
        public Task<bool> UpdateRefreshToken(string email, string refreshToken, CancellationToken cancellationToken = default(CancellationToken));
        public Task<bool> UpdatePasswordResetToken(string email, string passwordResetToken, CancellationToken cancellationToken = default(CancellationToken));
        public Task<bool> DeleteAccountAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
        public Task<Account> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}