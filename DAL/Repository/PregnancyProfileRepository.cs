using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace DAL.Repository
{
    public class PregnancyProfileRepository
    {
        private readonly PregnancyTrackingSystemContext _context;

        public PregnancyProfileRepository(PregnancyTrackingSystemContext context)
        {
            _context = context;
        }

        public List<PregnancyProfile> GetAll()
        {
            return _context.PregnancyProfiles
                .Include(p => p.User)
                .Include(p => p.FetalMeasurements)
                .ToList();
        }

        public PregnancyProfile GetById(int id)
        {
            return _context.PregnancyProfiles
                .Include(p => p.User)
                .Include(p => p.FetalMeasurements)
                .FirstOrDefault(p => p.Id == id);
        }

        public List<PregnancyProfile> GetByUserId(int userId)
        {
            return _context.PregnancyProfiles
                .Include(p => p.FetalMeasurements)
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
        }

        public void Add(PregnancyProfile profile)
        {
            _context.PregnancyProfiles.Add(profile);
            _context.SaveChanges();
        }

        public void Update(PregnancyProfile profile)
        {
            _context.PregnancyProfiles.Update(profile);
            _context.SaveChanges();
        }

        public void Delete(PregnancyProfile profile)
        {
            _context.PregnancyProfiles.Remove(profile);
            _context.SaveChanges();
        }
    }
}