using System.Text.Json;
using TcpProtocol;

namespace TcpServer;

public static class Data
{
    private static readonly PgSqlRepo _repo = new();

    public static (MessageType, string countries) GetCountries() =>
       (MessageType.Ok, JsonSerializer.Serialize(_repo.GetCountries()));

    public static (MessageType, string cities) GetCities(int code) =>
        (MessageType.Ok, JsonSerializer.Serialize(_repo.GetCities(code)));
}
