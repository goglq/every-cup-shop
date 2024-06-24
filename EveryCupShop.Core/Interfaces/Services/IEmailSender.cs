namespace EveryCupShop.Core.Interfaces.Services;

public interface IEmailSender
{
    Task SendEmail(string name, string email, string subject, string htmlMessage);
}