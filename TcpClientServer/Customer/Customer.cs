namespace TcpClientServer;

public class Customer : BaseCustomer
{
    public string Country { get; init; }
    public string City { get; init; }
    private List<Account> _accounts = [];

    public Customer(
        string firstName,
        string lastName,
        string email,
        string street,
        string ssn,
        string pin,
        string country,
        string city
) : base(firstName, lastName, email, street, ssn, pin)
    {
        Country = country;
        City = city;
    }

    public override string ToString() =>
        $"Name: {FirstName} {LastName}, Address: {Country}-{City}-{Street}, Ssn: {Ssn}, Pin: {Pin}";
}
