using EveryCupShop.Core.Configs;
using MailKit.Net.Smtp;
using EveryCupShop.Core.Interfaces.Services;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace EveryCupShop.Core.Services;

public class MimeEmailSender : IEmailSender
{
    private readonly SmtpEmailSenderConfig _config;

    public MimeEmailSender(IOptions<SmtpEmailSenderConfig> config)
    {
        _config = config.Value;
    }

    public async Task SendEmail(string name, string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_config.SenderName, _config.SenderEmail));
        message.To.Add(new MailboxAddress(name, email));

        message.Body = new TextPart("html")
        {
            Text = htmlMessage
        };

        using var client = new SmtpClient();
        await client.ConnectAsync(_config.Host, _config.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_config.Username, _config.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}