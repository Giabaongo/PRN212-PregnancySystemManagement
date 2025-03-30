using DAL.Repository;

namespace BLL.Services;

public class CommentService
{
    private readonly CommentRepository _commentRepository;

    public CommentService()
    {
        _commentRepository = new CommentRepository();
    }

    //public IEnumerable<Comment> GetAllComments()
    //{
    //    return _commentRepository.GetAll();
    //}

    //public Comment? GetCommentById(int id)
    //{
    //    return _commentRepository.GetById(id);
    //}

    //public IEnumerable<Comment> GetCommentsByPost(int postId)
    //{
    //    return _commentRepository.GetCommentsByPost(postId);
    //}

    //public IEnumerable<Comment> GetCommentsByUser(int userId)
    //{
    //    return _commentRepository.GetCommentsByUser(userId);
    //}

    //public void CreateComment(Comment comment)
    //{
    //    _commentRepository.Add(comment);
    //}

    //public void UpdateComment(Comment comment)
    //{
    //    _commentRepository.Update(comment);
    //}

    //public void DeleteComment(int id)
    //{
    //    var comment = _commentRepository.GetById(id);
    //    if (comment != null)
    //    {
    //        _commentRepository.Remove(comment);
    //    }
    //}
}