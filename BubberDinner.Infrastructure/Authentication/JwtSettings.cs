namespace BubberDinner.Infrastructure.Authentication;

public class JwtSettings
{
    public string Secret { get; init; } = default!;
    public int ExpiryMinutes { get; init; }
    public string Issuer { get; init; } = default!;
    public string Audience { get; init; } = default!;
}