using Dapper;
using MySql.Data.MySqlClient;

namespace DomShtor.DAL;

public static class DbHelper
{
    public static string ConnString = "server=127.0.0.1;userid=root;password=123zxc456vbnQS;database=domshtor;Allow User Variables=True";
    public static async Task<int> ExecuteScalarAsync(string sql, object model)
    {
        using (var connection = new MySqlConnection(ConnString))
        {
            await connection.OpenAsync();

            return await connection.QueryFirstOrDefaultAsync(sql, model);
        }
    }

    public static async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
    {
        using (var connection = new MySqlConnection(ConnString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<T>(sql, model);
        }
    }
}