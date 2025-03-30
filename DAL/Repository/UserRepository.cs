using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace DAL.Repository
{
    public class UserRepository
    {
        private readonly PregnancyTrackingSystemContext _context;

        public UserRepository()
        {
            _context = new PregnancyTrackingSystemContext();
        }

        public List<User> GetAll()
        {
            return _context.Users
                .Include(u => u.PregnancyProfiles)
                .Include(u => u.Posts)
                .Include(u => u.Comments)
                .Include(u => u.Appointments)
                .ToList();
        }

        public User GetById(int id)
        {
            return _context.Users
                .Include(u => u.PregnancyProfiles)
                .Include(u => u.Posts)
                .Include(u => u.Comments)
                .Include(u => u.Appointments)
                .FirstOrDefault(u => u.Id == id);
        }

        public User GetByEmail(string email)
        {
            return _context.Users
                .Include(u => u.PregnancyProfiles)
                .FirstOrDefault(u => u.Email == email);
        }

        public List<User> GetByUserType(string userType)
        {
            return _context.Users
                .Where(u => u.UserType == userType)
                .ToList();
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        public User? GetUserLogin(string email, string password)
        {
            return _context.Users.FirstOrDefault(e=>e.Email == email && e.Password == password);
        }
    }
} 