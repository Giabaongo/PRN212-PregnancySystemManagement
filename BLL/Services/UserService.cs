using DAL.Models;
using DAL.Repository;

namespace BLL.Services;

public class UserService //: IUserService
{
    private readonly UserRepository _userRepository;

    public UserService()
    {
        _userRepository = new UserRepository();
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAll();
    }

    //public User? GetUserById(int id)
    //{
    //    return _userRepository.GetById(id);
    //}

    //public User? GetUserByEmail(string email)
    //{
    //    return _userRepository.GetByEmail(email);
    //}

    //public IEnumerable<User> GetUsersByType(string userType)
    //{
    //    return _userRepository.GetUsersByType(userType);
    //}

    //public void CreateUser(User user)
    //{
    //    _userRepository.Add(user);
    //}

    //public void UpdateUser(User user)
    //{
    //    _userRepository.Update(user);
    //}

    //public void DeleteUser(int id)
    //{
    //    var user = _userRepository.GetById(id);
    //    if (user != null)
    //    {
    //        _userRepository.Remove(user);
    //    }
    //}
    public User? GetUser(string email, string password)
    {
        return _userRepository.GetUser(email, password);
    }
}