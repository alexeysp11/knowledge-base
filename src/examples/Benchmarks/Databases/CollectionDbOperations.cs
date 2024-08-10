using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SQLite;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Examples.Benchmarks.GarbageCollectionCases;

/// <summary>
/// Class for testing perfomance of the operations with collections for different DB providers.
/// </summary>
[MemoryDiagnoser]
public class CollectionDbOperations
{
    private object m_object = new object();
    private IReadOnlyList<Person> m_people;

    public CollectionDbOperations()
    {
        m_people = new List<Person>
        {
            new Person { Name = "Person1", Description = "Description1" },
            new Person { Name = "Person2", Description = "Description2" },
            new Person { Name = "Person3", Description = "Description3" },
            new Person { Name = "Person4", Description = "Description4" },
            new Person { Name = "Person5", Description = "Description5" },
            new Person { Name = "Person6", Description = "Description6" },
            new Person { Name = "Person7", Description = "Description7" }
        };
    }

    [Benchmark]
    public void DapperInsertCollection()
    {
        var sb = new StringBuilder();
        sb.Append("insert into DapperPerson (name) values ");
        for (int i = 0; i < m_people.Count; i++)
        {
            sb.Append($"('{m_people[i].Name}')");
            sb.Append(i == m_people.Count - 1 ? ";" : ",");
        }
        using (var connection = new SQLiteConnection(@"Data Source=data\CollectionOperations.db"))
        {
            connection.Execute(sb.ToString());
        }
    }
    
    // This method does not work properly since it throws exception "SQLite Error 19: 'UNIQUE constraint failed: EfCorePerson.id'".
    [Benchmark]
    public void EfCoreInsertCollection()
    {
        lock (m_object)
        {
            using (var missionContext = new CollectionOperationsContext())
            {
                missionContext.EfCorePerson.AddRange(m_people);
                missionContext.SaveChanges();
            }
        }
    }

    public class CollectionOperationsContext : DbContext
    {
        public DbSet<Person> EfCorePerson { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=data\CollectionOperations.db");
        }
    }

    public class Person
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }
    }
}