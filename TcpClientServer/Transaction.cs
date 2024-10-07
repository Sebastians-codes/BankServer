namespace TcpClientServer;

public readonly struct Transaction(Customer customer, string amount)
{
    public Customer Customer { get; init; } = customer;
    public string Amount { get; init; } = amount;
}
