using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Concepts.Examples.PgStoredFunctionsEfCore.Contexts;
using Concepts.Examples.PgStoredFunctionsEfCore.Models;

namespace Concepts.Examples.PgStoredFunctionsEfCore.DataProcessing;

public class DbDataProcessing
{
    private DbContextOptions<DbDataProcessingContext> m_contextOptions;

    public DbDataProcessing(DbContextOptions<DbDataProcessingContext> contextOptions)
    {
        m_contextOptions = contextOptions;
    }

    public void CallProcessData(int deleteFromProcessedData)
    {
        using var context = new DbDataProcessingContext(m_contextOptions);
        var parameterValue = new NpgsqlParameter("param", deleteFromProcessedData);
        context.Database.ExecuteSqlRaw("SELECT ProcessData(@param)", parameterValue);
    }

    public long CallProcessDataAndReturnResult(int deleteFromProcessedData, int insertProcessedData)
    {
        using var context = new DbDataProcessingContext(m_contextOptions);
        var deleteFromProcessedDataValue = new NpgsqlParameter("deleteFromProcessedData", deleteFromProcessedData);
        var insertProcessedDataValue = new NpgsqlParameter("insertProcessedData", insertProcessedData);
        
        var result = context.ProcessedDataResult
            .FromSqlRaw("SELECT * FROM ProcessAndReturnResult(@deleteFromProcessedData, @insertProcessedData)", deleteFromProcessedDataValue, insertProcessedDataValue)
            .ToList();
        
        if (result.Count > 0)
        {
            return result.First().Result;
        }

        return 0;
    }
}