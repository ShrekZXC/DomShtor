using System.Transactions;
using DomShtor.BL.Auth;
using DomShtor.DAL.Models;
using DomShtorTest.Helpers;

namespace DomShtorTest;

public class CurrentUserTest: Helpers.BaseTest
{
    [Test]
    public async Task BaseRegistrationTest()
    {
        using (TransactionScope scope = Helper.CreateTransactionScope())
        {
            await CreateAndAuthUser();

            bool isLoggedIn = await this.currentUser.IsLoggedIn();
            Assert.True(isLoggedIn);
            
            _webCoookie.Delete(AuthConstants.SessionCookieName);
            _dbSession.ResetSessionСache();

            isLoggedIn = await this.currentUser.IsLoggedIn();
            Assert.True(isLoggedIn);
            
            _webCoookie.Delete(AuthConstants.SessionCookieName);
            _webCoookie.Delete(AuthConstants.RememberMeCookieName);
            _dbSession.ResetSessionСache();

            isLoggedIn = await this.currentUser.IsLoggedIn();
            Assert.False(isLoggedIn);
        }
    }

    public async Task<int> CreateAndAuthUser()
    {
        string email = Guid.NewGuid().ToString() + "@text.com";

        int userId = await Auth.CreateUser(
            new UserModel()
            {
                Email = email,
                Password = "qwer1234",
                FirstName = "FirstName",
                SecondName = "SecondName",
                LastName = "LastName"
            }
        );
        return await Auth.Authenticate(email, "qwer1234", true);
    }
}