using System;
using System.Collections.Generic;
using DAL.Models;
using DAL.Repository;

namespace BLL.Services
{
    public class PostService
    {
        private readonly PostRepository _postRepository;

        public PostService(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public List<Post> GetAllPosts()
        {
            return _postRepository.GetAllPosts();
        }

        public Post GetPostById(int id)
        {
            return _postRepository.GetPostById(id);
        }

        public void CreatePost(string title, string content, int userId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Post title cannot be empty");
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Post content cannot be empty");

            var post = new Post
            {
                Title = title,
                Content = content,
                UserId = userId,
                CreatedAt = DateTime.Now
            };

            _postRepository.AddPost(post);
        }

        public void AddComment(int postId, string content, int userId)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Comment content cannot be empty");

            var comment = new Comment
            {
                PostId = postId,
                Content = content,
                UserId = userId,
                CreatedAt = DateTime.Now
            };

            _postRepository.AddComment(comment);
        }

        public void DeletePost(int postId, int userId)
        {
            var post = _postRepository.GetPostById(postId);
            if (post == null)
                throw new ArgumentException("Post not found");
            if (post.UserId != userId)
                throw new UnauthorizedAccessException("You are not authorized to delete this post");

            _postRepository.DeletePost(postId);
        }

        public void DeleteComment(int commentId, int userId)
        {
            var comment = _postRepository.GetCommentById(commentId);
            if (comment == null)
                throw new ArgumentException("Comment not found");
            if (comment.UserId != userId)
                throw new UnauthorizedAccessException("You are not authorized to delete this comment");

            _postRepository.DeleteComment(commentId);
        }
    }
}