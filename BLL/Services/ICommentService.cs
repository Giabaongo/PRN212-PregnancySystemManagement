using DAL.Models;

namespace BLL.Services;

public interface ICommentService
{
    IEnumerable<Comment> GetAllComments();
    Comment? GetCommentById(int id);
    IEnumerable<Comment> GetCommentsByPost(int postId);
    IEnumerable<Comment> GetCommentsByUser(int userId);
    void CreateComment(Comment comment);
    void UpdateComment(Comment comment);
    void DeleteComment(int id);
}