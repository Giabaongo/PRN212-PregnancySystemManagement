using DAL.Models;
using DAL.Repositories;

namespace BLL.Services
{
    public class MembershipService
    {
        private readonly MembershipRepository _membershipRepository;

        public MembershipService(MembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }

        public async Task<Membership?> GetActiveMembershipByUserId(int userId)
        {
            return await _membershipRepository.GetByUserId(userId);
        }

        public async Task<List<MembershipPlan>> GetAllPlans()
        {
            return await _membershipRepository.GetAllPlans();
        }

        public async Task<MembershipPlan?> GetPlanById(int planId)
        {
            return await _membershipRepository.GetPlanById(planId);
        }

        public async Task<Membership> SubscribeToPlan(int userId, int planId)
        {
            var plan = await _membershipRepository.GetPlanById(planId);
            if (plan == null)
            {
                throw new Exception("Plan not found");
            }

            var membership = new Membership
            {
                UserId = userId,
                PlanId = planId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(plan.Duration),
                Status = "Active",
                CreatedAt = DateTime.Now
            };

            return await _membershipRepository.Create(membership);
        }

        public async Task<Membership> CreateMembership(Membership membership)
        {
            return await _membershipRepository.Create(membership);
        }

        public async Task<Membership> UpdateMembership(Membership membership)
        {
            return await _membershipRepository.Update(membership);
        }

        public async Task DeleteMembership(int id)
        {
            await _membershipRepository.Delete(id);
        }
    }
}