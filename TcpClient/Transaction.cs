namespace Client1;

public readonly struct Transaction(Account account, string amount)
{
    public Account Account { get; init; } = account;
    public string Amount { get; init; } = amount;
}
