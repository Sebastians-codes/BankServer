namespace TcpServer;

public readonly struct Country(int id, string name, int code)
{
    public int Id { get; init; } = id;
    public string Name { get; init; } = name;
    public int Code { get; init; } = code;
}
