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
        public Task<bool> UpdateRefreshTokenAsync(string email, string refreshToken, CancellationToken cancellationToken = default(CancellationToken));
        public Task<bool> UpdatePasswordResetTokenAsync(string email, string passwordResetToken, CancellationToken cancellationToken = default(CancellationToken));
        public Task<Account?> FindUserByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default(CancellationToken));
        public Task<Account?> FindUserByPasswordResetTokenAsync(string passwordResetToken, CancellationToken cancellationToken = default(CancellationToken));
        public Task ChangePasswordAsync(string email, string password, CancellationToken cancellationToken = default(CancellationToken));
    }
}