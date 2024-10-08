using System.Text.Json;
using TcpClientServer.Server;
using TcpProtocol;

namespace TcpClientServer;

public class CustomerFactory(Input input, ClientServer server)
{
    public DbCustomer Make()
    {
        string firstName = input.GetFirstName();
        string lastName = input.GetLastName();
        string email = input.GetEmail();

        List<Country> countries =
        JsonSerializer.Deserialize<List<Country>>(
            server.SendMessage(MessageType.Data, "country").Content)!;

        (int id, int code) country = input.GetCountry(countries);

        List<City> cities =
        JsonSerializer.Deserialize<List<City>>(
            server.SendMessage(MessageType.Data, $"city,{country.code}").Content)!;

        int city = input.GetCity(cities);
        string street = input.GetStreet();
        string ssn = input.GetSsn();
        string pin = input.GetPin();

        DbCustomer newCustomer = new(firstName, lastName, email, street, ssn, pin, country.id, city);

        return newCustomer;
    }
}
