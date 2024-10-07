namespace TcpServer;

public class Customer

{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Country { get; init; }
    public string City { get; init; }
    public string Street { get; init; }
    public string Ssn { get; init; }
    public string Pin { get; init; }

    public Customer(
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
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Country = country;
        City = city;
        Street = street;
        Ssn = ssn;
        Pin = pin;
    }



}
