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
            var savedEntity = await _context.Accounts.AddAsync(user);
            await _context.SaveChangesAsync();
            return savedEntity.Entity;
        }

        public async Task<Core.Entities.Account?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}