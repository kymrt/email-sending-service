using email_sending_service.Utilities;
using NUnit.Framework;

namespace email_sending_service.Test.UtilitiesTests
{
    public class ValidationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("asd@com")]
        [TestCase("asd@.com")]
        [TestCase("asd.com")]
        [TestCase("asd.asd.com")]
        [TestCase("asd@asd@asd.com")]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void CheckEmailAddress_InvalidMailAddress_False(string emailAddress)
        {
            Validations validations = new Validations();
            bool result = validations.CheckEmailAddress(emailAddress);
            Assert.IsFalse(result);
        }

        [TestCase("asd@asd.com")]
        public void CheckEmailAddress_ValidMailAddress_True(string emailAddress)
        {
            Validations validations = new Validations();
            bool result = validations.CheckEmailAddress(emailAddress);
            Assert.IsTrue(result);
        }
    }
}
