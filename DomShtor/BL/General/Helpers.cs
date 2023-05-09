using System.Transactions;

namespace DomShtor.BL.General;

public static class Helpers
{
    public static int? StringToIntDef(string str, int? def)
    {
        int value;
        if (int.TryParse(str, out value))
            return value;
        return def;
    }
    
    public static Guid? StringToGuidDef(string str)
    {
        Guid value;
        if (Guid.TryParse(str, out value))
            return value;
        return null;
    }
    
    // Когда дебажим добавить времени, иначе сессия умрет
    static public TransactionScope CreateTransactionScope(int seconds = 60)
    {
        return new TransactionScope(
            TransactionScopeOption.Required,
            new TimeSpan(0,0,seconds),
            TransactionScopeAsyncFlowOption.Enabled);
    }
}