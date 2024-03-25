using Microsoft.AspNetCore.Identity.UI.Services;

namespace bai4_webbanhang.Utilitys
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }

    }
}
