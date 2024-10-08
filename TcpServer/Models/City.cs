namespace TcpServer;

public readonly struct City(int id, string name)
{
    public int Id { get; init; } = id;
    public string Name { get; init; } = name;
}
