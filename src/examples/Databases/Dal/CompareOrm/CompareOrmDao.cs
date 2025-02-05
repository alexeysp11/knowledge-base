using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace KnowledgeBase.Examples.Databases.Dal.CompareOrm;

public static class CompareOrmDao
{
    public static void Run(string connectionString)
    {
        Stopwatch sw;

        // Data adapter.
        sw = Stopwatch.StartNew();
        ExecuteQueryDA(connectionString, "select Id, Name from CompareOrmDA");
        sw.Stop();
        System.Console.WriteLine($"Data adapter: {sw.ElapsedMilliseconds}");

        // Dapper.
        sw = Stopwatch.StartNew();
        ExecuteQueryDapper(connectionString, "select Id, Name from CompareOrmDapper");
        sw.Stop();
        System.Console.WriteLine($"Dapper: {sw.ElapsedMilliseconds}");
    }

    private static DataTable ExecuteQueryDA(string connectionString, string cmdText)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var dt = new DataTable();
            var da = new NpgsqlDataAdapter(cmdText, connection);
            da.Fill(dt);
            return dt;
        }
    }

    private static List<CompareOrmObjectType> ExecuteQueryDapper(string connectionString, string cmdText)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            return connection.Query<CompareOrmObjectType>(cmdText).ToList();
        }
    }
}