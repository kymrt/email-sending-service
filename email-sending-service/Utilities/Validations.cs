using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace email_sending_service.Utilities
{
    public class Validations
    {
        public bool CheckEmailAddress(string emailAddress) 
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                return false;

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(emailAddress);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
