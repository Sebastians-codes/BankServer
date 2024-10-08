namespace TcpClientServer;

public interface IUserInteraction
{
    public string ReadLine();
    public char ReadKey(bool print);
}
