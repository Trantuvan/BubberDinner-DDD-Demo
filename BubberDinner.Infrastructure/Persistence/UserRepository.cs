using BubberDinner.Application.Common.Persistence;
using BubberDinner.Domain.Entities;

namespace BubberDinner.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
  /* UserRepo register IoC AsScope
   * Make list user static to create one time only
  */
  private static readonly List<User> _users = new();
  public void Add(User user)
  {
    _users.Add(user);
  }

  public User? GetUserByEmail(string email)
  {
    return _users.SingleOrDefault(x => x.Email == email);
  }
}
