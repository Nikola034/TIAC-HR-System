using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.User
{
    public class AccountRepository : IAccountRepository
    {
        private AccountDbContext _context;
        public AccountRepository(AccountDbContext context)
        {
            _context = context;
        }
        public async Task<Core.Entities.Account> CreateAsync(Core.Entities.Account user, CancellationToken cancellationToken = default(CancellationToken))
        {
            var savedEntity = await _context.Accounts.AddAsync(user,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return savedEntity.Entity;
        }

        public async Task<Core.Entities.Account?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email,cancellationToken);
        }
        public async Task<bool> UpdateRefreshTokenAsync(string email, string refreshToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            var savedEntity = await _context.Accounts.FirstOrDefaultAsync(x => x.Email == email,cancellationToken);
            savedEntity.RefreshToken = refreshToken;
            savedEntity.RefreshTokenValidTo = DateTime.Now.AddHours(24);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<bool> UpdatePasswordResetTokenAsync(string email, string passwordResetToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            var savedEntity = await _context.Accounts.FirstOrDefaultAsync(x => x.Email == email,cancellationToken);
            savedEntity.PasswordResetToken = passwordResetToken;
            savedEntity.PasswordResetTokenValidTo = DateTime.Now.AddHours(24);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteAccountAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Core.Entities.Account> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public async Task<Core.Entities.Account?> FindUserByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.Accounts.
                FirstOrDefaultAsync(x => x.RefreshToken == refreshToken && x.RefreshTokenValidTo > DateTime.Now, cancellationToken);
        }
        
        public async Task<Core.Entities.Account?> FindUserByPasswordResetTokenAsync(string passwordResetToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.Accounts.
                FirstOrDefaultAsync(x => x.PasswordResetToken == passwordResetToken && x.PasswordResetTokenValidTo > DateTime.Now, cancellationToken);
        }
        public async Task BlockUnblockUser(string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            var existingAccount = await _context.Accounts.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
            if (existingAccount.IsBlocked)
            {
                existingAccount.IsBlocked = false;
            }
            else
            {
                existingAccount.IsBlocked = true;
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task ChangePasswordAsync(string email, string password, CancellationToken cancellationToken = default(CancellationToken))
        {
            var existingAccount = await _context.Accounts.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
            existingAccount.Password = password;
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<IEnumerable<Core.Entities.Account>?> GetAccountsByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        {
            var accounts = await _context.Accounts.
            Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
            return accounts;
        }
    }
}