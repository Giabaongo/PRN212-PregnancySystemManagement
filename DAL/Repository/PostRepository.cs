using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class PostRepository
    {
        private readonly PregnancyTrackingSystemContext _context;

        public PostRepository()
        {
            _context = new PregnancyTrackingSystemContext();
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
        }

        public Post GetPostById(int id)
        {
            return _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                .FirstOrDefault(p => p.Id == id);
        }

        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public void DeletePost(int postId)
        {
            var post = _context.Posts.Find(postId);
            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
            }
        }

        public void DeleteComment(int commentId)
        {
            var comment = _context.Comments.Find(commentId);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
            }
        }

        public Comment GetCommentById(int commentId)
        {
            return _context.Comments
                .Include(c => c.User)
                .FirstOrDefault(c => c.Id == commentId);
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