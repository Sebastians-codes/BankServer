namespace TcpClientServer;

public abstract class BaseCustomer(
    string firstName,
    string lastName,
    string email,
    string street,
    string ssn,
    string pin
)
{
    public string FirstName { get; init; } = firstName;
    public string LastName { get; init; } = lastName;
    public string Email { get; init; } = email;
    public string Street { get; init; } = street;
    public string Ssn { get; init; } = ssn;
    public string Pin { get; init; } = pin;
}
