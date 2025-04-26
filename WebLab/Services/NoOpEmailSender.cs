using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace WebLab.Services
{
    public class NoOpEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Не делаем ничего (заглушка для email-сервисов)
            return Task.CompletedTask;
        }
    }
}