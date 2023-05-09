using System.Transactions;
using DomShtorTest.Helpers;

namespace DomShtorTest;

public class SessionTest: Helpers.BaseTest
{
    [Test]
    [NonParallelizable]
    public async Task CreateSessionTest()
    {
        using (TransactionScope scope = Helper.CreateTransactionScope())
        {
            ((TestCookie)_webCoookie).Clear();
            _dbSession.ResetSessionСache();
            var session = await _dbSession.GetSession();

            var dbSessoion = await _dbSessionDal.Get(session.DbSessionId);

            Assert.NotNull(dbSessoion, "Session shoule not be null");

            Assert.That(dbSessoion.DbSessionId, Is.EqualTo(session.DbSessionId));

            var session2 = await _dbSession.GetSession();
            Assert.That(dbSessoion.DbSessionId, Is.EqualTo(session2.DbSessionId));
        }
    }

    [Test]
    [NonParallelizable]
    public async Task CreateAuthorizedSessionTest()
    {
        using (TransactionScope scope = Helper.CreateTransactionScope())
        {
            ((TestCookie)_webCoookie).Clear();
            _dbSession.ResetSessionСache();
            var session = await _dbSession.GetSession();
            await _dbSession.SetUserId(10);

            var dbSessoion = await _dbSessionDal.Get(session.DbSessionId);

            Assert.NotNull(dbSessoion, "Session shoule not be null");
            Assert.That(dbSessoion.UserId!, Is.EqualTo(10));

            Assert.That(dbSessoion.DbSessionId, Is.EqualTo(session.DbSessionId));

            var session2 = await _dbSession.GetSession();
            Assert.That(dbSessoion.DbSessionId, Is.EqualTo(session2.DbSessionId));

            int? userid = await this.currentUser.GetCurrentUserId();
            Assert.That(userid, Is.EqualTo(10));
        }
    }

}