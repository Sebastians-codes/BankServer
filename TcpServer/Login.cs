using System.Text.Json;
using TcpProtocol;

namespace TcpServer;

public static class Login
{
    private static readonly PgSqlRepo _repo = new();

    public static (MessageType Type, string Message) Validate(string content)
    {
        Customer? customer = _repo.GetAccount(JsonSerializer.Deserialize<LoginCredentials>(content));

        if (customer == null)
        {
            return (MessageType.Err, "Invalid Credentials");
        }

        return (MessageType.Ok, JsonSerializer.Serialize(customer));
    }

    public static (MessageType Type, string Message) CreateAccount(string content)
    {
        var customer = JsonSerializer.Deserialize<DbCustomer>(content);

        if (_repo.CustomerExist(customer))
        {
            return (MessageType.Err, "Cannot create an account with this number");
        }

        _repo.InsertCustomer(customer);

        return (MessageType.Ok, "Account created");
    }
}
