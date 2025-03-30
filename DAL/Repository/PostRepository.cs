using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository;

public class PostRepository : GenericRepository<Post>, IPostRepository
{
    public PostRepository(PregnancyTrackingSystemContext context) : base(context)
    {
    }

    public IEnumerable<Post> GetPostsByUser(int userId)
    {
        return _dbSet.Where(p => p.UserId == userId).ToList();
    }

    public Post? GetPostWithComments(int postId)
    {
        return _dbSet
            .Include(p => p.Comments)
            .ThenInclude(c => c.User)
            .FirstOrDefault(p => p.Id == postId);
    }
}