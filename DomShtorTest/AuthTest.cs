using System.Net;
using System.Transactions;
using DomShtorTest.Helpers;
using System.Transactions;
using DomShtor.BL;
using DomShtor.DAL.Models;


namespace DomShtorTest;

public class AuthTest : Helpers.BaseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task BaseAuthTest()
    {
        using (TransactionScope scope = Helper.CreateTransactionScope())
        {
            string email = Guid.NewGuid().ToString() + "@test.com";

            // create user
            int userId = await Auth.CreateUser(
                new UserModel()
                {
                    Email = email,
                    Password = "qwer1234",
                    FirstName = "Name",
                    SecondName = "SecondName",
                    LastName = "LastName"
                });

            Assert.Throws<AuthorizationException>(delegate
            {
                Auth.Authenticate("werewr", "1111", false).GetAwaiter().GetResult();
            });

            Assert.Throws<AuthorizationException>(delegate
            {
                Auth.Authenticate(email, "1111", false).GetAwaiter().GetResult();
            });

            Assert.Throws<AuthorizationException>(delegate
            {
                Auth.Authenticate("werewr", "qwer1234", false).GetAwaiter().GetResult();
            });

            await Auth.Authenticate(email, "qwer1234", false);
        }
    }
}