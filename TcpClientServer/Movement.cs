namespace TcpClientServer;

public readonly struct Movement(decimal amount, string? note = null)
{
    public decimal Amount { get; init; } = amount;
    public DateTime Date { get; init; } = DateTime.Now;
    public string? Note { get; init; } = note;
}
