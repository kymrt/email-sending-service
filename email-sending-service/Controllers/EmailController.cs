using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using email_sending_service.Data;
using email_sending_service.Models;
using email_sending_service.Utilities;
using email_sending_service.Filters;

namespace email_sending_service.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmailController : ControllerBase
    {
        private readonly EmailDataContext _context;
        private readonly string DatabaseMessage = "Email addresses have been added to the database and will be sent within 5 minutes.";

        public EmailController(EmailDataContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        [EmailActionFilter]
        public async Task<ActionResult> SendEmail(List<string> emailAddressList)
        {
            IMailService sender = new SendGridMailService();
            var resultMailService = await sender.SendMailAsync(emailAddressList);
            if (resultMailService)
            {
                return Ok();
            }
            else
            {
                var result = AddEmailAddressToDb(emailAddressList);
                return result.Result;
            }
        }
        
        [HttpPost]
        [EmailActionFilter]
        public async Task<ActionResult> AddEmailAddressToDb(List<string> emailAddressList)
        {
            List<EmailInfo> emailInfos = new List<EmailInfo>();
            MapMailAddressToModel(emailAddressList, emailInfos);
            _context.EmailInfo.AddRange(emailInfos);
            await _context.SaveChangesAsync();
            return Ok(DatabaseMessage);
        }

        private void MapMailAddressToModel(List<string> emailAddressList, List<EmailInfo> emailInfos)
        {
            foreach (var item in emailAddressList)
            {
                emailInfos.Add(new EmailInfo
                {
                    EmailAddress = item,
                    RegisterDate = DateTime.Now,
                    State = 0
                });
            }
        }
    }
}
