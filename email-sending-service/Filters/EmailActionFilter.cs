using System.Collections.Generic;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Mvc;
using email_sending_service.Utilities;
using MvcFilters = Microsoft.AspNetCore.Mvc.Filters;

namespace email_sending_service.Filters
{
    public class EmailActionFilter : FilterAttribute, MvcFilters.IActionFilter
    {
        private readonly string EmailValidationErrorMessage = "You entered a wrong formatted email.Pls try again";
        public void OnActionExecuted(MvcFilters.ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(MvcFilters.ActionExecutingContext filterContext)
        {
            List<string> emailAddressList = (List<string>)filterContext.ActionArguments["emailAddressList"];
            foreach (var emailAddress in emailAddressList)
            {
                bool isEmailAddressValid = new Validations().CheckEmailAddress(emailAddress);
                if (!isEmailAddressValid)
                {                    
                    filterContext.Result = new NotFoundObjectResult(EmailValidationErrorMessage);
                }
            }
        }
    }
}
