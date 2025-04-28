using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class FetalMeasurementRepository
    {
        private readonly PregnancyTrackingSystemContext _context;

        public FetalMeasurementRepository()
        {
            _context = new PregnancyTrackingSystemContext();
        }

        public List<FetalMeasurement> GetAll()
        {
            return _context.FetalMeasurements
                .Include(f => f.Profile)
                .ToList();
        }

        public FetalMeasurement GetById(int id)
        {
            return _context.FetalMeasurements
                .Include(f => f.Profile)
                .FirstOrDefault(f => f.Id == id);
        }

        public List<FetalMeasurement> GetByProfileId(int profileId)
        {
            return _context.FetalMeasurements
                .Where(f => f.ProfileId == profileId)
                .OrderByDescending(f => f.CreatedAt)
                .ToList();
        }

        public FetalGrowthStandard GetGrowthStandardByWeek(int week)
        {
            return _context.FetalGrowthStandards
                .FirstOrDefault(s => s.WeekNumber == week);
        }

        public void Add(FetalMeasurement measurement)
        {
            _context.FetalMeasurements.Add(measurement);
            _context.SaveChanges();
        }

        public void Update(FetalMeasurement measurement)
        {
            _context.FetalMeasurements.Update(measurement);
            _context.SaveChanges();
        }

        public void Delete(FetalMeasurement measurement)
        {
            _context.FetalMeasurements.Remove(measurement);
            _context.SaveChanges();
        }
    }
}