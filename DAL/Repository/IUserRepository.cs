using DAL.Models;

namespace DAL.Repository;

public interface IUserRepository : IGenericRepository<User>
{
    User? GetByEmail(string email);
    IEnumerable<User> GetUsersByType(string userType);
    User? GetUser(string email, string password);
}