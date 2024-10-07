namespace TcpServer;

public readonly struct Transaction(Customer account, string amount)
{
    public Customer Account { get; init; } = account;
    public string Amount { get; init; } = amount;
}
