using DAL.Models;
using DAL.Repository;

namespace BLL.Services
{
    public class MembershipPlanService
    {
        private readonly MembershipPlanRepository _membershipPlanRepository;

        public MembershipPlanService()
        {
            _membershipPlanRepository = new MembershipPlanRepository();
        }

        public IEnumerable<MembershipPlan> GetAllPlans()
        {
            return _membershipPlanRepository.GetAllPlans();
        }

        public MembershipPlan? GetPlanById(int id)
        {
            return _membershipPlanRepository.GetPlanById(id);
        }

        public void AddPlan(MembershipPlan plan)
        {
            _membershipPlanRepository.AddPlan(plan);
        }

        public void UpdatePlan(MembershipPlan plan)
        {
            _membershipPlanRepository.UpdatePlan(plan);
        }

        public void DeletePlan(int id)
        {
            _membershipPlanRepository.DeletePlan(id);
        }
    }
}