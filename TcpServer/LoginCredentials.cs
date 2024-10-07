namespace TcpServer;

public readonly struct LoginCredentials(string ssn, string pin)
{
    public readonly string Ssn { get; init; } = ssn;
    public readonly string Pin { get; init; } = pin;
}
