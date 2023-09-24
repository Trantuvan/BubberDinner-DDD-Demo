using BubberDinner.Application.Common.Interfaces.Authentication;
using BubberDinner.Application.Common.Persistence;
using BubberDinner.Domain.Entities;

namespace BubberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IUserRepository _userRepository;
  public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
  {
    _jwtTokenGenerator = jwtTokenGenerator;
    _userRepository = userRepository;
  }

  public AuthenticationResult Login(string email, string password)
  {
    //* 1 Validate the user exist

    if (_userRepository.GetUserByEmail(email) is not User user)
    {
      throw new Exception("User with given email does not exist");
    }

    //* 2 Validate the password correct

    if (!user.Password.Equals(password))
    {
      throw new Exception("Invalid password");
    }

    //* 3 Create Jwt token

    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthenticationResult(user, token);
  }

  public AuthenticationResult Register(string firstName, string lastName, string email, string password)
  {
    //* 1 Validate the user doesn't exist

    if (_userRepository.GetUserByEmail(email) is not null)
    {
      throw new Exception("User with given email already exist");
    }

    //* 2 Create new user (generate unique ID GUID) & Persist to DB

    var user = new User
    {
      Email = email,
      Password = password,
      FirstName = firstName,
      LastName = lastName
    };

    _userRepository.Add(user);

    //* Generate JWT token

    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthenticationResult(user, token);
  }
}