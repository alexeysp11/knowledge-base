using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Studying.Leetcode.Orm 
{
    /// <summary>
    /// Testing EF Core 
    /// </summary>
    public class Blogging : Studying.Leetcode.ILeetcodeProblem 
    {
        private class BloggingContext : DbContext
        {
            public DbSet<Blog> Blogs { get; set; }
            public DbSet<Post> Posts { get; set; }

            public string DbPath { get; }

            public BloggingContext()
            {
                var folder = System.IO.Path.Join(System.AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\data\\sqlite3");
                DbPath = System.IO.Path.Join(folder, "blogging.db");
            }

            protected override void OnConfiguring(DbContextOptionsBuilder options)
                => options.UseSqlite($"Data Source={DbPath}");
        }

        private class Blog
        {
            public int BlogId { get; set; }
            public string Url { get; set; }

            public List<Post> Posts { get; } = new List<Post>(); 
        }

        private class Post
        {
            public int PostId { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }

            public int BlogId { get; set; }
            public Blog Blog { get; set; }
        }

        public void Execute()
        {
            System.Console.WriteLine("Testing EF Core (ORM approach)\n".ToUpper()); 

            using var db = new BloggingContext();

            // Note: This sample requires the database to be created before running.
            System.Console.WriteLine($"Database path: {db.DbPath}.");

            // Create
            System.Console.WriteLine("Inserting a new blog");
            db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
            db.SaveChanges();

            // Read
            System.Console.WriteLine("Querying for a blog");
            var blog = db.Blogs
                .OrderBy(b => b.BlogId)
                .First();

            // Update
            System.Console.WriteLine("Updating the blog and adding a post");
            blog.Url = "https://devblogs.microsoft.com/dotnet";
            blog.Posts.Add(new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
            db.SaveChanges();
        }
    }
}