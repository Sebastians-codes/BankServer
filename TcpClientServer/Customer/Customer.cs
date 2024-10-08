namespace TcpClientServer;

public class Customer(
    string firstName,
    string lastName,
    string email,
    string street,
    string ssn,
    string pin,
    string country,
    string city
) : BaseCustomer(firstName, lastName, email, street, ssn, pin)
{
    public string Country { get; init; } = country;
    public string City { get; init; } = city;

    public override string ToString() =>
        $"Name: {FirstName} {LastName}, Address: {Country}-{City}-{Street}, Ssn: {Ssn}, Pin: {Pin}";
}
