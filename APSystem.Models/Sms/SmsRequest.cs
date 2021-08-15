using System;

namespace APSystem.Models.Sms
{
    public class SmsRequest
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Message { get; set; }
    }
}
