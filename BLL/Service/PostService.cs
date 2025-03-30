using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Repository;

namespace BLL.Service
{
    public class PostService
    {
        private readonly PostRepository _postRepository;

        public PostService( )
        {
            _postRepository = new PostRepository();
        }

        public List<Post> GetAllPosts()
        {
            return _postRepository.GetAll();
        }

        public Post GetPostById(int id)
        {
            return _postRepository.GetById(id);
        }

        public List<Post> GetPostsByUserId(int userId)
        {
            return _postRepository.GetByUserId(userId);
        }

        public void CreatePost(Post post)
        {
            _postRepository.Add(post);
        }

        public void UpdatePost(Post post)
        {
            _postRepository.Update(post);
        }

        public void DeletePost(Post post)
        {
            _postRepository.Delete(post);
        }
    }
} 