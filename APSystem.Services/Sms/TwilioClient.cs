using System;
using System.Threading.Tasks;
using APSystem.Configuration.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Twilio.Clients;
using Twilio.Http;
using SystemHttpClient = System.Net.Http.HttpClient;

namespace APSystem.Services.Sms
{
    public class TwilioClient:ITwilioRestClient
    {
        private readonly ITwilioRestClient _client;
        IOptions<SmsSettings> _smsSettings;
        public TwilioClient( SystemHttpClient client
        , IOptions<SmsSettings> smsSettings)
        {
            // customize
            _smsSettings = smsSettings;
            client.DefaultRequestHeaders.Add("X-Custom-Header", "HCAS SMS");

            _client = new TwilioRestClient(
                _smsSettings.Value.AccountSid,
               _smsSettings.Value.AuthToken,
                httpClient: new SystemNetHttpClient(client));
        }

        public Response Request(Request request) => _client.Request(request);
        public Task<Response> RequestAsync(Request request) => _client.RequestAsync(request);
        public string AccountSid => _client.AccountSid;
        public string Region => _client.Region;
        public Twilio.Http.HttpClient HttpClient => _client.HttpClient;

    }
}
