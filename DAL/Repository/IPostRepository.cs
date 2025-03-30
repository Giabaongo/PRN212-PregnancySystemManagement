using DAL.Models;

namespace DAL.Repository;

public interface IPostRepository
{
    IEnumerable<Post> GetPostsByUser(int userId);
    Post? GetPostWithComments(int postId);
}