using email_sending_service.Data;
using email_sending_service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace email_sending_service.Utilities
{
    public class CronJobService
    {
        private readonly EmailDataContext _context;
        private readonly IMailService _mailService;

        public CronJobService(EmailDataContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }

        public void SendMailFromDatabase()
        {
            List<EmailInfo> emailInfos = _context.EmailInfo.ToList();
            var unsentMailList = emailInfos.Where(x => x.State == 0).ToList();
            var result = _mailService.SendMailAsync(unsentMailList.Select(x => x.EmailAddress).ToList());
            if (result.Result)
            {
                unsentMailList = unsentMailList.Select(c => { c.State = 1; c.SentDate = DateTime.Now; return c; }).ToList();
                _context.EmailInfo.UpdateRange(unsentMailList);
                _context.SaveChanges();
            }
        }
    }
}
