using System.Net;
using System.Net.Mail;
using Application.Common.Services;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;

    public EmailService()
    {
        _smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("username", "password"),
            EnableSsl = true,
        };
    }
    
    public void SendPasswordResetEmail(string email, string passwordResetToken, CancellationToken ct)
    {
        var body = "Click the following link in order to change you password: http://localhost:5000/passwordreset/" +
                   passwordResetToken;
        _smtpClient.SendAsync("tiac-email",email,"HR System - Password reset",body, ct);
    }
}