using System.Linq;

namespace PredicateExtensions.UnitTests.Resources
{
    public class BlogStub
    {
        public IQueryable<Post> GetPosts()
        {
            //arrange
            Post[] posts = 
            {
                new Post { Title = "First Post", Content = "Lorem Ipsum" },
                new Post { Title = "Second Post", Content = "Lorem Ipsum" },
                new Post { Title = "Third Post", Content = "keyword" },
                new Post { Title = "Fourth Post", Content = "Lorem Ipsum" }
            };

            // The IQueryable data to query.
            return posts.AsQueryable<Post>();
        }
    }
}