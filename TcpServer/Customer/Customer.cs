namespace TcpServer;

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
}
