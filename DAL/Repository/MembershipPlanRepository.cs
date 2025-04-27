using DAL.Models;

namespace DAL.Repository
{
    public class MembershipPlanRepository
    {
        private readonly PregnancyTrackingSystemContext _context;

        public MembershipPlanRepository()
        {
            _context = new PregnancyTrackingSystemContext();
        }

        public IEnumerable<MembershipPlan> GetAllPlans()
        {
            return _context.MembershipPlans.ToList();
        }

        public MembershipPlan? GetPlanById(int id)
        {
            return _context.MembershipPlans.FirstOrDefault(p => p.Id == id);
        }

        public void AddPlan(MembershipPlan plan)
        {
            _context.MembershipPlans.Add(plan);
            _context.SaveChanges();
        }

        public void UpdatePlan(MembershipPlan plan)
        {
            _context.MembershipPlans.Update(plan);
            _context.SaveChanges();
        }

        public void DeletePlan(int id)
        {
            var plan = GetPlanById(id);
            if (plan != null)
            {
                _context.MembershipPlans.Remove(plan);
                _context.SaveChanges();
            }
        }
    }
}