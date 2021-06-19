using System;

namespace APSystem.Configuration.Settings
{
    public class EmailSettings
    {
        /// <summary>
        /// This properties is used for Email Configuration in EmailSettings
        /// </summary>
        /// <value></value>
      public string MailServer { get; set; }
      public int MailPort { get; set; }
      public string SenderName { get; set; }
      public string Sender { get; set; }
      public string Password { get; set; }
    }
}