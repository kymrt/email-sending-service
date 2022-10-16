using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace email_sending_service.Utilities
{
    public class SendGridMailService : IMailService
    {
        public async Task<bool> SendMailAsync(List<string> emailAdressList)
        {
            string apiKey = Environment.GetEnvironmentVariable("SENDGRID_KEY");
            SendGridClient client = new SendGridClient(apiKey);
            EmailAddress fromAddress = new EmailAddress(Environment.GetEnvironmentVariable("FROM_EMAIL_ADDRESS"));
            string templateId = Environment.GetEnvironmentVariable("TEMPLATE_ID");
            List<EmailAddress> toAddresses = GetEmailList(emailAdressList);
            string name = Environment.GetEnvironmentVariable("NAME");
            var dynamicTemplateData = new Dictionary<string, string>
            {
                { "your_name", name }
            };
            var msg = MailHelper.CreateSingleTemplateEmailToMultipleRecipients(fromAddress, toAddresses, templateId, dynamicTemplateData);
            var response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode;
        }

        private List<EmailAddress> GetEmailList(List<string> emailAdressList)
        {
            List<EmailAddress> result = new List<EmailAddress>();
            foreach (var item in emailAdressList)
            {
                result.Add(new EmailAddress(item));
            }
            return result;
        }
    }
}
