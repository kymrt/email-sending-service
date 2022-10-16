using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace email_sending_service.Utilities
{
    public interface IMailService
    {
        Task<bool> SendMailAsync(List<string> emailAdressList);
    }
}
