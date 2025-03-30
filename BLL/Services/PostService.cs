using DAL.Models;
using DAL.Repository;

namespace BLL.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public IEnumerable<Post> GetAllPosts()
    {
        return _postRepository.GetAll();
    }

    public Post? GetPostById(int id)
    {
        return _postRepository.GetById(id);
    }

    public Post? GetPostWithComments(int id)
    {
        return _postRepository.GetPostWithComments(id);
    }

    public IEnumerable<Post> GetPostsByUser(int userId)
    {
        return _postRepository.GetPostsByUser(userId);
    }

    public void CreatePost(Post post)
    {
        _postRepository.Add(post);
    }

    public void UpdatePost(Post post)
    {
        _postRepository.Update(post);
    }

    public void DeletePost(int id)
    {
        var post = _postRepository.GetById(id);
        if (post != null)
        {
            _postRepository.Remove(post);
        }
    }
}