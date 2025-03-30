using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository;

public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    public CommentRepository(PregnancyTrackingSystemContext context) : base(context)
    {
    }

    public IEnumerable<Comment> GetCommentsByPost(int postId)
    {
        return _dbSet
            .Include(c => c.User)
            .Where(c => c.PostId == postId)
            .ToList();
    }

    public IEnumerable<Comment> GetCommentsByUser(int userId)
    {
        return _dbSet
            .Include(c => c.Post)
            .Where(c => c.UserId == userId)
            .ToList();
    }
}