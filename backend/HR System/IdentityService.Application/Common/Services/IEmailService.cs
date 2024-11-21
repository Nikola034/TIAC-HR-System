namespace Application.Common.Services;

public interface IEmailService
{
    public Task SendPasswordResetEmail(string email, string passwordResetToken);
}