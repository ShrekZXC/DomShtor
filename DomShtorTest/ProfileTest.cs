using System.Transactions;
using DomShtor.BL.Profile;
using DomShtor.DAL.Models;
using DomShtorTest.Helpers;

namespace DomShtorTest;

public class ProfileTest: Helpers.BaseTest
{
    [Test]
    public async Task AddTest()
    {
        using (TransactionScope scope = Helper.CreateTransactionScope())
        {
            string email = Guid.NewGuid() + "@test.com";
            
            await _profile.AddOrUpdate(
                new ProfileModel()
                {
                    UserId = 19,
                    FirstName = "Иван",
                    SecondName = "Иванов",
                    LastName = "Иванович",
                    Email = email
                });

            var result = await _profile.GetByUserId(19);
            Assert.That(result.FirstName, Is.EqualTo("Иван"));
            Assert.That(result.SecondName, Is.EqualTo("Иванов"));
            Assert.That(result.LastName, Is.EqualTo("Иванович"));
            Assert.That(result.UserId, Is.EqualTo(19));
        }
    }
    
    [Test]
    public async Task UpdateTest()
    {
        using (TransactionScope scope = Helper.CreateTransactionScope())
        {
            string email = Guid.NewGuid() + "@test.com";

            var profileModel = new ProfileModel()
            {
                UserId = 19,
                FirstName = "Иван",
                SecondName = "Иванов",
                LastName = "Иванович",
                Email = email
            };

            await _profile.AddOrUpdate(profileModel);

            profileModel.FirstName = "Иван1";

            await _profile.AddOrUpdate(profileModel);

            var result = await _profile.GetByUserId(19);

            Assert.That(result.FirstName, Is.EqualTo("Иван1"));
            Assert.That(result.UserId, Is.EqualTo(19));
        }
    }
}