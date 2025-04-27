using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class MembershipRepository
    {
        private readonly PregnancyTrackingSystemContext _context;

        public MembershipRepository(PregnancyTrackingSystemContext context)
        {
            _context = context;
        }

        public async Task<Membership?> GetByUserId(int userId)
        {
            return await _context.Memberships
                .Include(m => m.Plan)
                .FirstOrDefaultAsync(m => m.UserId == userId && m.Status == "Active");
        }

        public async Task<List<MembershipPlan>> GetAllPlans()
        {
            return await _context.MembershipPlans.ToListAsync();
        }

        public async Task<MembershipPlan?> GetPlanById(int planId)
        {
            return await _context.MembershipPlans.FindAsync(planId);
        }

        public async Task<Membership> Create(Membership membership)
        {
            _context.Memberships.Add(membership);
            await _context.SaveChangesAsync();
            return membership;
        }

        public async Task<Membership> Update(Membership membership)
        {
            _context.Memberships.Update(membership);
            await _context.SaveChangesAsync();
            return membership;
        }

        public async Task Delete(int id)
        {
            var membership = await _context.Memberships.FindAsync(id);
            if (membership != null)
            {
                _context.Memberships.Remove(membership);
                await _context.SaveChangesAsync();
            }
        }
    }
}