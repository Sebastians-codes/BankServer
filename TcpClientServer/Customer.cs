namespace TcpClientServer;

public struct Customer(
    string firstName,
    string lastName,
    string email,
    string country,
    string city,
    string street,
    string ssn,
    string pin
)
{
    public string FirstName { get; init; } = firstName;
    public string LastName { get; init; } = lastName;
    public string Email { get; init; } = email;
    public string Country { get; init; } = country;
    public string City { get; init; } = city;
    public string Street { get; init; } = street;
    public string Ssn { get; init; } = ssn;
    public string Pin { get; init; } = pin;
}
