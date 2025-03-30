using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Repository;

namespace BLL.Service
{
    public class CommentService
    {
        private readonly CommentRepository _commentRepository;

        public CommentService( )
        {
            _commentRepository = new CommentRepository();
        }

        public List<Comment> GetAllComments()
        {
            return _commentRepository.GetAll();
        }

        public Comment GetCommentById(int id)
        {
            return _commentRepository.GetById(id);
        }

        public List<Comment> GetCommentsByPostId(int postId)
        {
            return _commentRepository.GetByPostId(postId);
        }

        public void CreateComment(Comment comment)
        {
            _commentRepository.Add(comment);
        }

        public void UpdateComment(Comment comment)
        {
            _commentRepository.Update(comment);
        }

        public void DeleteComment(Comment comment)
        {
            _commentRepository.Delete(comment);
        }
    }
} 