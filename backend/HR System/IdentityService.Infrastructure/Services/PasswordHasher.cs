using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Services;
using BCrypt.Net;

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