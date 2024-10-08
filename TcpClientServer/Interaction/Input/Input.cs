namespace TcpClientServer;

public class Input(IUserInterface userInterface, IUserInteraction userInteraction)
{
    private readonly Ssn _ssn = new(userInterface, userInteraction);
    private readonly Pin _pin = new(userInterface, userInteraction);
    private readonly Name _name = new(userInterface, userInteraction);
    private readonly Email _email = new(userInterface, userInteraction);
    private readonly CountrySelector _country = new(userInterface, userInteraction);
    private readonly CitySelector _city = new(userInterface, userInteraction);
    private readonly Street _street = new(userInterface, userInteraction);

    public string GetFirstName() =>
        _name.Get("Enter your first name -> ");

    public string GetLastName() =>
        _name.Get("Enter your last name -> ");

    public string GetEmail() =>
        _email.Get();

    public (int id, int code) GetCountry(List<Country> countries) =>
        _country.Get(countries);

    public int GetCity(List<City> cities) =>
        _city.Get(cities);

    public string GetStreet() =>
        _street.Get();

    public string GetSsn() =>
        _ssn.Get();

    public string GetPin() =>
        _pin.Get();
}
