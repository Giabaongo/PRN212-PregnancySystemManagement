using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository;

public class PostRepository : IPostRepository
{
    private readonly PregnancyTrackingSystemContext _dbSet;

    public PostRepository()
    {
        _dbSet = new PregnancyTrackingSystemContext();
    }

    public IEnumerable<Post> GetPostsByUser(int userId)
    {
        return _dbSet.Posts.Where(p => p.UserId == userId).ToList();
    }

    public Post? GetPostWithComments(int postId)
    {
        return _dbSet.Posts
            .Include(p => p.Comments)
            .ThenInclude(c => c.User)
            .FirstOrDefault(p => p.Id == postId);
    }
}