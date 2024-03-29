﻿using System.Transactions;

namespace DomShtorTest.Helpers;

static public class Helper
{
    static public TransactionScope CreateTransactionScope(int seconds = 999999)
    {
        return new TransactionScope(
            TransactionScopeOption.Required,
            new TimeSpan(0,0,seconds),
            TransactionScopeAsyncFlowOption.Enabled);
    }
}