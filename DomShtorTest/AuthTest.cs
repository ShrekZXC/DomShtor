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
            int userId = await _authBl.CreateUser(
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
                _authBl.Authenticate("werewr", "1111", false).GetAwaiter().GetResult();
            });

            Assert.Throws<AuthorizationException>(delegate
            {
                _authBl.Authenticate(email, "1111", false).GetAwaiter().GetResult();
            });

            Assert.Throws<AuthorizationException>(delegate
            {
                _authBl.Authenticate("werewr", "qwer1234", false).GetAwaiter().GetResult();
            });

            await _authBl.Authenticate(email, "qwer1234", false);
        }
    }
}