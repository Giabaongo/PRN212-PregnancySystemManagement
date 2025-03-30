using DAL.Models;

namespace BLL.Services;

public interface IPostService
{
    IEnumerable<Post> GetAllPosts();
    Post? GetPostById(int id);
    Post? GetPostWithComments(int id);
    IEnumerable<Post> GetPostsByUser(int userId);
    void CreatePost(Post post);
    void UpdatePost(Post post);
    void DeletePost(int id);
}