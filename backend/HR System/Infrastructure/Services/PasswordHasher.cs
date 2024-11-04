using Application.Common.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
