using Discussion.BLL.Services.Interfaces;
using Discussion.Models.DTO_s.EmailDTO_s;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;

namespace Discussion.BLL.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public void SendRegistrationEmail(string to)
    {
        var emailDTO = new EmailDTO();

        emailDTO.To = to;
        emailDTO.Subject = "Registration";
        emailDTO.Body = "<h1>You have successfully registered.</h1>" +
            "<h2>Have fun and learn new things!</h2>";

        SendEmail(emailDTO);
    }

    private void SendEmail(EmailDTO emailDTO)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailConfiguration:EmailUsername").Value));
        email.To.Add(MailboxAddress.Parse(emailDTO.To));
        email.Subject = emailDTO.Subject;
        email.Body = new TextPart(TextFormat.Html) { Text = emailDTO.Body };

        using var smtp = new SmtpClient();
        smtp.Connect(_config.GetSection("EmailConfiguration:EmailHost").Value, 587, SecureSocketOptions.StartTls);
        smtp.Authenticate(_config.GetSection("EmailConfiguration:EmailUsername").Value, _config.GetSection("EmailConfiguration:EmailPassword").Value);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}
