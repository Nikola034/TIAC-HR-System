using System.Net;
using System.Net.Mail;
using Application.Common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;
    private readonly IConfiguration _configuration;
    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
        _smtpClient = new SmtpClient(_configuration["SmtpClientConfig:Host"])
        {
            Port = Int32.Parse(_configuration["SmtpClientConfig:Port"]),
            Credentials = new NetworkCredential(_configuration["SmtpClientConfig:Host"], _configuration["SmtpClientConfig:Password"]),
            EnableSsl = false,
        };
    }
    
    public async Task SendPasswordResetEmail(string email, string passwordResetToken)
    {
        var mail = _configuration["SmtpClientConfig:SenderEmail"];
        var password = _configuration["SmtpClientConfig:Password"];
        var port = int.Parse(_configuration["SmtpClientConfig:Port"]);
        var host = _configuration["SmtpClientConfig:Host"];
        var client = new SmtpClient(host, port)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(mail, password)
        };
        
        var body =
            "Click the following link in order to change your password: http://localhost:5000/passwordReset/" +
            passwordResetToken;
        var mailToSend = new MailMessage()
        {
            From = new MailAddress(mail),
            Subject = "HR System - Password reset",
            Body = body,
            IsBodyHtml = false
        };
        mailToSend.To.Add(email);
        await client.SendMailAsync(mailToSend);
    }
}
