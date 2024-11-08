namespace Application.Common.Services;

public interface IEmailService
{
    public void SendPasswordResetEmail(string email, string passwordResetToken, CancellationToken cancellationToken = default(CancellationToken));
}