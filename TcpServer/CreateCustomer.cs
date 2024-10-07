using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TcpServer;

public class CreateCustomer(
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
    public string FirstName { get; init; } = firstName;
    public string LastName { get; init; } = lastName;
    public string Email { get; init; } = email;
    public int CountryId { get; init; } = countryId;
    public int CityId { get; init; } = cityId;
    public string Street { get; init; } = street;
    public string Ssn { get; init; } = ssn;
    public string Pin { get; init; } = pin;
}
