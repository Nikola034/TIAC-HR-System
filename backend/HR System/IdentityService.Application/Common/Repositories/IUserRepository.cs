using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken));
        public Task<User> CreateAsync(User user, CancellationToken cancellationToken = default(CancellationToken));

    }
}
