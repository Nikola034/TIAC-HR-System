using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;

namespace Application.Common.Services
{
    public interface IJwtService
    {
        public Task<TokenResponse> GenerateTokensAsync(string username);
        public string GenerateByteToken();
    }
}