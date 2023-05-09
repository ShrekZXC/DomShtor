using System.Transactions;
using DomShtorTest.Helpers;

namespace DomShtorTest;

public class TokenTest : Helpers.BaseTest
{
    [Test]
    public async Task BaseTokenTest()
    {
        using (TransactionScope scope = Helper.CreateTransactionScope())
        {
            var tokenId = await _userTokenDal.Create(10);
            var userid = await _userTokenDal.Get(tokenId);
            Assert.That(userid, Is.EqualTo(10));
        }
    }
}