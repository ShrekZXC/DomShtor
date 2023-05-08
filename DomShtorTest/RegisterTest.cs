using System.Transactions;
using DomShtorTest.Helpers;
using System.Transactions;
using DomShtor.BL;
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
            await Auth.ValidateEmail(email);

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
            
            Assert.Greater(userId, 0);

            var userDalResult = await _authDal.GetUser(userId);
            Assert.That(email, Is.EqualTo(userDalResult.Email));
            Assert.NotNull(userDalResult.Salt);
            
            var userByEmailDalResult = await _authDal.GetUser(email);
            Assert.That(email, Is.EqualTo(userByEmailDalResult.Email));
            Assert.NotNull(userByEmailDalResult.Salt);
            
            Assert.Throws<DuplicateEmailException>(delegate { Auth.ValidateEmail(email).GetAwaiter().GetResult(); });

            string encPassword = _encrypt.HashPassword("qwer1234", userByEmailDalResult.Salt);
            Assert.That(encPassword, Is.EqualTo(userByEmailDalResult.Password));

        }
    }
}