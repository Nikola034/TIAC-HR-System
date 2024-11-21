using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenValidTo { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenValidTo { get; set; }
        public bool IsBlocked { get; set; }

    }
}