using Dapper;
using Npgsql;

namespace WokerServiceXplatform;

public class Worker : BackgroundService
{
    private object _obj = new object();
    private readonly ILogger<Worker> _logger;
    private readonly string _connectionString;

    public Worker(ILogger<Worker> logger, AppInitConfigs appInitConfigs)
    {
        _logger = logger;
        _connectionString = appInitConfigs.DbConnectionString;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            string message = $"Worker running at: {DateTimeOffset.Now}";

            _logger.LogInformation(message);

            var sqlQuery = GetInsertLogSql(message);
            lock (_obj)
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Execute(sqlQuery);
                }
            }

            await Task.Delay(1000, stoppingToken);
        }
    }

    private string GetInsertLogSql(string message)
    {
        return $"insert into \"WorkerServiceLogs\" (\"Message\", \"ModuleName\") values ('{message}', 'WokerServiceXplatform');";
    }
}
