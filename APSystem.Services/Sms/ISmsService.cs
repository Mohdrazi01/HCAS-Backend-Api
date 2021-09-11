using System;
using System.Threading.Tasks;
using APSystem.Models.Sms;

namespace APSystem.Services.Sms
{
    public interface ISmsService
    {
        Task<SmSResponse> SendSmsAsync(SmsRequest sms);
    }
}
