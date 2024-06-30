using System.Data;
using System.Data.SQLite;
using System.Text;
using Dapper;
using Concepts.Examples.SimpleWebService.Core.Models;

namespace Concepts.Examples.SimpleWebService.Core.DAL;

/// <summary>
/// Allows you to add information about cached values to the database.
/// </summary>
public class CachedServerValueDAL
{
    private readonly object m_lock = new object();
    private readonly bool m_deleteCacheBeforeInserting;
    private readonly string m_connectionString;
    private readonly string m_deleteSQL = "DELETE FROM CachedServerValues;";
    private readonly string m_insertSQL = "INSERT INTO CachedServerValues (OrderNo, Code, Value) VALUES ";
    private readonly string m_selectSQL = "SELECT c.OrderNo, c.Code, c.Value FROM CachedServerValues c WHERE {0};";

    /// <summary>
    /// Default constructor.
    /// </summary>
    public CachedServerValueDAL(AppInitConfigs appInitConfigs)
    {
        m_deleteCacheBeforeInserting = appInitConfigs.DeleteCacheBeforeInserting;
        m_connectionString = appInitConfigs.DbConnectionString;
    }

    /// <summary>
    /// Method for inserting the specified cached values into DB.
    /// </summary>
    public void InsertCachedValues(IReadOnlyList<CachedServerValueEntry> values)
    {
        if (values == null)
            throw new System.ArgumentNullException(nameof(values));
        if (values.Count == 0)
            return;
        
        var sqlQuery = GenerateInsertSqlByCachedValues(values);

        lock (m_lock)
        {
            using (var connection = new SQLiteConnection(m_connectionString))
            {
                connection.Execute(sqlQuery.Query, sqlQuery.Parameters);
            }
        }
    }

    /// <summary>
    /// Method to get cached values from database.
    /// </summary>
    public IReadOnlyList<CachedServerValue> GetCachedValues(int? code)
    {
        var sqlQuery = GenerateSelectSqlByCode(code);

        IReadOnlyList<CachedServerValue> result;
        lock (m_lock)
        {
            using (var connection = new SQLiteConnection(m_connectionString))
            {
                result = connection.Query<CachedServerValue>(sqlQuery).ToList();
            }
        }
        return result;
    }

    /// <summary>
    /// Method for generating an SQL query for inserting data about cached values.
    /// </summary>
    private (string Query, DynamicParameters Parameters) GenerateInsertSqlByCachedValues(IReadOnlyList<CachedServerValueEntry> values)
    {
        var queryParameters = new DynamicParameters();
        var stringBuilder = new StringBuilder();
        
        if (m_deleteCacheBeforeInserting)
            stringBuilder.Append(m_deleteSQL);
        stringBuilder.Append(m_insertSQL);

        for (int i = 0; i < values.Count; i++)
        {
            if (i > 0)
                stringBuilder.Append(", ");
            stringBuilder.Append($"(@CachedOrderNo{i}, @CachedCode{i}, @CachedValue{i})");
            
            queryParameters.Add($"CachedOrderNo{i}", i + 1);
            queryParameters.Add($"CachedCode{i}", values[i].Code);
            queryParameters.Add($"CachedValue{i}", values[i].Value);
        }

        return (stringBuilder.ToString(), queryParameters);
    }

    /// <summary>
    /// Method for generating an SQL query for getting data about cached values.
    /// </summary>
    private string GenerateSelectSqlByCode(int? code)
    {
        return string.Format(m_selectSQL, (code.HasValue ? $"c.Code = {code}" : "1 = 1"));
    }
}