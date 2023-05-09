using System.Transactions;
using DomShtorTest.Helpers;

namespace DomShtorTest;

public class SessionTest: Helpers.BaseTest
{
    [Test]
    public async Task CreateSessionTest()
    {
        using (TransactionScope scope = Helper.CreateTransactionScope())
        {
            var session = await this._dbSession.GetSession();

            var dbSession = await this._dbSessionDal.Get(session.DbSessionId);
            
            Assert.NotNull(dbSession);
            
            Assert.That(dbSession.DbSessionId, Is.EqualTo(session.DbSessionId));

            var session2 = await this._dbSession.GetSession();
            
            Assert.That(dbSession.DbSessionId, Is.EqualTo(session2.DbSessionId ));
        }
    }

}