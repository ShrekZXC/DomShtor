using System.Transactions;
using DomShtorTest.Helpers;
using System.Transactions;
using DomShtor.DAL.Models;


namespace DomShtorTest;

public class RegisterTest: Helpers.BaseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task BaseRegistration()
    {
        using (TransactionScope scope = Helper.CreateTransactionScope())
        {
            string email = Guid.NewGuid() + "@test.com";

            // validate: should not be in the DB
            var emailValidationResult = await _authBl.ValidateEmail(email);
            Assert.IsNull(emailValidationResult);

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
            
            Assert.Greater(userId, 0);

            var userDalResult = await _authDal.GetUser(userId);
            Assert.That(email, Is.EqualTo(userDalResult.Email));
            Assert.NotNull(userDalResult.Salt);
            
            var userByEmailDalResult = await _authDal.GetUser(email);
            Assert.That(email, Is.EqualTo(userByEmailDalResult.Email));
            Assert.NotNull(userByEmailDalResult.Salt);
            
            emailValidationResult = await _authBl.ValidateEmail(email);
            Assert.IsNotNull(emailValidationResult);

            string encPassword = _encrypt.HashPassword("qwer1234", userByEmailDalResult.Salt);
            Assert.That(encPassword, Is.EqualTo(userByEmailDalResult.Password));

        }
    }
}