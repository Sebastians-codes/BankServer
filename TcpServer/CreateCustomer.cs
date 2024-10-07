using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TcpServer;

public class CreateCustomer

{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public int CountryId { get; init; }
    public int CityId { get; init; }
    public string Street { get; init; }
    public string Ssn { get; init; }
    public string Pin { get; init; }

    public CreateCustomer(
        string firstName,
        string lastName,
        string email,
        int countryId,
        int cityId,
        string street,
        string ssn,
        string pin
    )
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        CountryId = countryId;
        CityId = cityId;
        Street = street;
        Ssn = ssn;
        Pin = pin;
    }
}
