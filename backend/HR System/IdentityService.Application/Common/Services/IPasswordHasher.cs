using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Services
{
    public interface IPasswordHasher
    {
        public Task<string> HashPasswordAsync(string plainTextPassword, CancellationToken cancellationToken = default(CancellationToken));
        public Task<bool> VerifyPasswordAsync(string plainTextPassword, string password, CancellationToken cancellationToken = default(CancellationToken));
    }
}