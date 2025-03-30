using DAL.Models;

namespace DAL.Repository;

public interface IPostRepository : IGenericRepository<Post>
{
    IEnumerable<Post> GetPostsByUser(int userId);
    Post? GetPostWithComments(int postId);
}