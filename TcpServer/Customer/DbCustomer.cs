namespace TcpServer;

public class DbCustomer(
    string firstName,
    string lastName,
    string email,
    string street,
    string ssn,
    string pin,
    int countryId,
    int cityId
) : BaseCustomer(firstName, lastName, email, street, ssn, pin)
{
    public int CountryId { get; init; } = countryId;
    public int CityId { get; init; } = cityId;
}
