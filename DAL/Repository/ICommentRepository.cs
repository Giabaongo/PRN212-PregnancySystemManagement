using DAL.Models;

namespace DAL.Repository;

public interface ICommentRepository : IGenericRepository<Comment>
{
    IEnumerable<Comment> GetCommentsByPost(int postId);
    IEnumerable<Comment> GetCommentsByUser(int userId);
}