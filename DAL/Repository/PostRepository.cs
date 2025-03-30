using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace DAL.Repository
{
    public class PostRepository
    {
        private readonly PregnancyTrackingSystemContext _context;

        public PostRepository( )
        {
            _context = new PregnancyTrackingSystemContext();
        }

        public List<Post> GetAll()
        {
            return _context.Posts.Include(p => p.User).Include(p => p.Comments).ToList();
        }

        public Post GetById(int id)
        {
            return _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments)
                .FirstOrDefault(p => p.Id == id);
        }

        public List<Post> GetByUserId(int userId)
        {
            return _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments)
                .Where(p => p.UserId == userId)
                .ToList();
        }

        public void Add(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void Update(Post post)
        {
            _context.Posts.Update(post);
            _context.SaveChanges();
        }

        public void Delete(Post post)
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }
    }
} 