using System.Collections.Generic;
using System.Linq;
using MimeKit;

namespace APSystem.Models.Email
{
    public class EmailRequest
    {
        public EmailRequest(IEnumerable<string> to, string subject, string content, AttachmentCollection attachments)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => MailboxAddress.Parse(x)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public AttachmentCollection Attachments { get; set; }
    }
}