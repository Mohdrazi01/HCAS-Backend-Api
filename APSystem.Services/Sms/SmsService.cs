using System.Threading.Tasks;
using APSystem.Models.Sms;
using Microsoft.Extensions.Logging;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace APSystem.Services.Sms
{
    public class SmsService : BaseService<SmsService>, ISmsService
    {
        private readonly ITwilioRestClient _client;
        public SmsService(ITwilioRestClient client
        , ILogger<SmsService> logger
        , MetaData.IMetaDataService metaDataService) : base(metaDataService, logger)
        {
            _client = client;
        }
        async Task<SmSResponse> ISmsService.SendSmsAsync(SmsRequest sms)
        {
            SmSResponse smsResponse = new SmSResponse();
            var message = MessageResource.Create(
            to: new PhoneNumber(sms.To),
            from: new PhoneNumber(sms.From),
            body: sms.Message,
            client: _client); //
            return await Task.FromResult(smsResponse);
        }
    }
}
