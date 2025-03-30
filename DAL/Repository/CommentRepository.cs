using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace DAL.Repository
{
    public class CommentRepository
    {
        private readonly PregnancyTrackingSystemContext _context;

        public CommentRepository()
        {
            _context = new PregnancyTrackingSystemContext();
        }

        public List<Comment> GetAll()
        {
            return _context.Comments.Include(c => c.User).Include(c => c.Post).ToList();
        }

        public Comment GetById(int id)
        {
            return _context.Comments
                .Include(c => c.User)
                .Include(c => c.Post)
                .FirstOrDefault(c => c.Id == id);
        }

        public List<Comment> GetByPostId(int postId)
        {
            return _context.Comments
                .Include(c => c.User)
                .Where(c => c.PostId == postId)
                .ToList();
        }

        public void Add(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public void Update(Comment comment)
        {
            _context.Comments.Update(comment);
            _context.SaveChanges();
        }

        public void Delete(Comment comment)
        {
            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }
    }
} 