using Microsoft.EntityFrameworkCore;
using KnowledgeBase.Examples.PgStoredFunctionsEfCore.Models;

namespace KnowledgeBase.Examples.PgStoredFunctionsEfCore.Contexts;

/// <summary>
/// Represents the database context for Service Interaction in the application.
/// </summary>
public class DbDataProcessingContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the DbDataProcessingContext class with the specified options.
    /// </summary>
    /// <param name="options">The DbContextOptions to be used by the context.</param>
    public DbDataProcessingContext(DbContextOptions<DbDataProcessingContext> options) : base(options) { }

    /// <summary>
    /// Gets or sets the DbSet of Table1 entities in the context.
    /// </summary>
    public DbSet<Table1> Table1 { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of Table2 entities in the context.
    /// </summary>
    public DbSet<Table2> Table2 { get; set; }
    
    /// <summary>
    /// Gets or sets the DbSet of ProcessedData entities in the context.
    /// </summary>
    public DbSet<ProcessedData> ProcessedData { get; set; }
    
    /// <summary>
    /// Gets or sets the DbSet of ProcessedDataResult entities in the context.
    /// </summary>
    public DbSet<ProcessedDataResult> ProcessedDataResult { get; set; }
}