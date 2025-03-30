using DAL.Models;

namespace BLL.Services;

public interface IUserService
{
    IEnumerable<User> GetAllUsers();
    User? GetUserById(int id);
    User? GetUserByEmail(string email);
    IEnumerable<User> GetUsersByType(string userType);
    void CreateUser(User user);
    void UpdateUser(User user);
    void DeleteUser(int id);
    User? GetUser(string email, string password);
}