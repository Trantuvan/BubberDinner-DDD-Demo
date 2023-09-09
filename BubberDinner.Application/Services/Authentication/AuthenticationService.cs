using BubberDinner.Application.Common.Interfaces.Authentication;

namespace BubberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(Guid.NewGuid(),
                                        "firstname",
                                        "lastname",
                                        email,
                                        "token");
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        //* Check if the user already exists

        //* Create new user (generate unique ID GUID)
        Guid userId = Guid.NewGuid();

        //* Generate JWT token
        var token = _jwtTokenGenerator.GenerateToken(userId, firstName, lastName);

        return new AuthenticationResult(userId,
                                        firstName,
                                        lastName,
                                        email,
                                        token);
    }
}