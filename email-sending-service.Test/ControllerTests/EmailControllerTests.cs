using email_sending_service.Controllers;
using email_sending_service.Data;
using email_sending_service.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace email_sending_service.Test.ControllerTests
{
    public class EmailControllerTests
    {
        private List<string> _emailAddressList;

        [SetUp]
        public void Setup()
        {
            _emailAddressList = new List<string>
            {
                "asd@asd.com",
                "xyz@xyz.com"
            };
        }

        [Test]
        public void AddEmailAddressToDb_ValidMailAddress_True()
        {
            var options = new DbContextOptionsBuilder<EmailDataContext>().UseInMemoryDatabase(databaseName: "EmailInfoDatabase").Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new EmailDataContext(options))
            {
                context.EmailInfo.Add(new EmailInfo { Id = 1, EmailAddress = "test1@test.com", RegisterDate = DateTime.Now, State = 0 });
                context.EmailInfo.Add(new EmailInfo { Id = 2, EmailAddress = "test2@test.com", RegisterDate = DateTime.Now, State = 0 });
                context.EmailInfo.Add(new EmailInfo { Id = 3, EmailAddress = "test3@test.com", RegisterDate = DateTime.Now, State = 0 });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new EmailDataContext(options))
            {
                EmailController movieRepository = new EmailController(context);
                var test = movieRepository.AddEmailAddressToDb(_emailAddressList);
                Assert.IsTrue(test.IsCompletedSuccessfully);
            }
        }
    }
}
