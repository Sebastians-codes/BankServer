using System.Text.Json;
using TcpProtocol;

namespace TcpServer;

public static class Data
{
    private static readonly PgSqlRepo _repo = new();

    public static (MessageType, string) Get(string content)
    {
        string[] parts = content.Split(',');
        return parts[0] switch
        {
            "country" => GetCountries(),
            "city" => GetCities(int.Parse(parts[1]))
        };
    }

    private static (MessageType, string countries) GetCountries() =>
       (MessageType.Ok, JsonSerializer.Serialize(_repo.GetCountries()));

    private static (MessageType, string cities) GetCities(int code) =>
        (MessageType.Ok, JsonSerializer.Serialize(_repo.GetCities(code)));
}
