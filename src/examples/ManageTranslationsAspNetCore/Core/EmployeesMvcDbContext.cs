using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Examples.ManageTranslationsAspNetCore.Core;

public class EmployeesMvcDbContext : DbContext
{
    public EmployeesMvcDbContext(DbContextOptions<EmployeesMvcDbContext> options) : base(options) { }

    public DbSet<LanguageKeyValuePair> LanguageKeyValuePairs { get; set; }
}