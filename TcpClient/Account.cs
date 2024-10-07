namespace Client1;

public struct Account(string ssn, string pin)
{
    public string Ssn { get; init; } = ssn;
    public string Pin { get; init; } = pin;
}
