using System.Text.Json;
using TcpProtocl;

namespace TcpServer;

public static class Login
{
    private static readonly PgSqlRepo _repo = new();

    public static (MessageType Type, string Message) Validate(string content) =>
        _repo.GetAccount(JsonSerializer.Deserialize<Account>(content)) != null ?
        (MessageType.Ok, "Logged In") : (MessageType.Err, "Invalid Credentials");

    public static (MessageType Type, string Message) CreateAccount(string content)
    {
        var account = JsonSerializer.Deserialize<Account>(content);

        if (_repo.AccountExist(account))
        {
            return (MessageType.Err, "Cannot create an account with this number");
        }

        _repo.InsertAccount(account);

        return (MessageType.Ok, "Account created");
    }
}
