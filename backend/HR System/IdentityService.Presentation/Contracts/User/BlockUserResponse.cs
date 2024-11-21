namespace IdentityService.Presentation.Contracts.User
{
    public class BlockUserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenValidTo { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenValidTo { get; set; }
        public bool IsBlocked { get; set; }
    }
}
