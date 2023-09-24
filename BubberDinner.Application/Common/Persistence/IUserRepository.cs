using BubberDinner.Domain.Entities;

namespace BubberDinner.Application.Common.Persistence;

public interface IUserRepository
{
  void Add(User user);
  User? GetUserByEmail(string email);
}
