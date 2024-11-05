using Application.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.User
{
    public class UserRepository : IUserRepository
    {
        private MicroservicesDbContext _context;
        public UserRepository(MicroservicesDbContext context)
        {
            _context = context;
        }
        public async Task<Core.Entities.User> CreateAsync(Core.Entities.User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            var savedEntity = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return savedEntity.Entity;
        }

        public async Task<Core.Entities.User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Username == email);
        }
    }
}
