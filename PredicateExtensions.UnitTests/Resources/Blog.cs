using System.Collections.Generic;

namespace PredicateExtensions.UnitTests.Resources
{
    public class Blog
    {
        public int BlogId { get; set; }

        public string Name { get; set; }

        public virtual List<Post> Posts { get; set; }
    }
}