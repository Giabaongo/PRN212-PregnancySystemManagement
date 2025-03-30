using DAL.Models;
namespace DAL.Repository;

public class UserRepository : IUserRepository
{
    PregnancyTrackingSystemContext _dbSet;
    public UserRepository()
    {
        _dbSet = new PregnancyTrackingSystemContext();
    }

    public IEnumerable<User> GetAll()
    {
        return _dbSet.Users.ToList();
    }

    public User? GetByEmail(string email)
    {
        return _dbSet.Users.FirstOrDefault(u => u.Email == email);
    }

    public IEnumerable<User> GetUsersByType(string userType)
    {
        return _dbSet.Users.Where(u => u.UserType == userType).ToList();
    }
    public User? GetUser(string email, string password)
    {
        return _dbSet.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
    }
}