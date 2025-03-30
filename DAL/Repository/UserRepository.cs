using DAL.Models;

namespace DAL.Repository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(PregnancyTrackingSystemContext context) : base(context)
    {
    }

    public User? GetByEmail(string email)
    {
        return _dbSet.FirstOrDefault(u => u.Email == email);
    }

    public IEnumerable<User> GetUsersByType(string userType)
    {
        return _dbSet.Where(u => u.UserType == userType).ToList();
    }
    public User? GetUser(string email, string password)
    {
        return _dbSet.FirstOrDefault(u => u.Email == email && u.Password == password);
    }
}