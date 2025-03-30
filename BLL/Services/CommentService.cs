using DAL.Models;
using DAL.Repository;

namespace BLL.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public IEnumerable<Comment> GetAllComments()
    {
        return _commentRepository.GetAll();
    }

    public Comment? GetCommentById(int id)
    {
        return _commentRepository.GetById(id);
    }

    public IEnumerable<Comment> GetCommentsByPost(int postId)
    {
        return _commentRepository.GetCommentsByPost(postId);
    }

    public IEnumerable<Comment> GetCommentsByUser(int userId)
    {
        return _commentRepository.GetCommentsByUser(userId);
    }

    public void CreateComment(Comment comment)
    {
        _commentRepository.Add(comment);
    }

    public void UpdateComment(Comment comment)
    {
        _commentRepository.Update(comment);
    }

    public void DeleteComment(int id)
    {
        var comment = _commentRepository.GetById(id);
        if (comment != null)
        {
            _commentRepository.Remove(comment);
        }
    }
}