using Microsoft.EntityFrameworkCore;

namespace PredicateExtensions.UnitTests.Resources
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }
    }
}