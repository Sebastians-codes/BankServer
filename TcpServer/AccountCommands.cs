using System.Text.Json;
using TcpProtocl;

namespace TcpServer;

public static class AccountCommands
{
    private static readonly PgSqlRepo _repo = new();

    public static (MessageType type, string message) MakeTransaction(string content)
    {
        var transaction = JsonSerializer.Deserialize<Transaction>(content);
        _repo.InsertTransaction(transaction.Account.Ssn, transaction.Amount);

        return (MessageType.Ok, "Transaction has been made");
    }
}
