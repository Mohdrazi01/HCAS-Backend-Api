using System.Threading.Tasks;
using APSystem.Models.Email;

namespace APSystem.Services.Email
{
    public interface IEmailService
    {
         Task SendEmailAsync(EmailRequest emailRequest);
    }
}