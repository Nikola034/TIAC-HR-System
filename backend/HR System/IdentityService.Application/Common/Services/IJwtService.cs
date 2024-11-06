using Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Services
{
    public interface IJwtService
    {
        public Task<TokenResponse> GenerateTokensAsync(string username);
    }
}
