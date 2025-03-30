using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly PregnancyTrackingSystemContext _dbSet;

    public CommentRepository()
    {
        _dbSet = new PregnancyTrackingSystemContext();
    }

    public IEnumerable<Comment> GetCommentsByPost(int postId)
    {
        return _dbSet.Comments
            .Include(c => c.User)
            .Where(c => c.PostId == postId)
            .ToList();
    }

    public IEnumerable<Comment> GetCommentsByUser(int userId)
    {
        return _dbSet.Comments
            .Include(c => c.Post)
            .Where(c => c.UserId == userId)
            .ToList();
    }
}