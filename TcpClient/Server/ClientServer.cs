using System.Net.Sockets;
using TcpProtocl;

namespace Client1.Server;

public class ClientServer(string serverIp, int port, string secretPassword) : IDisposable
{
    private readonly string _secretPassword = secretPassword;
    private readonly string _serverIp = serverIp;
    private readonly int _port = port;
    private TcpClient? _client;
    private NetworkStream? _stream;
    private bool disposed = false;

    public void Connect()
    {
        try
        {
            _client = new TcpClient(_serverIp, _port);
            _stream = _client.GetStream();
            Console.WriteLine("Connected to server.");
        }
        catch (SocketException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public (object Type, string Content) SendMessage(MessageType type, string content, CommandType? commandType = null)
    {
        if (_stream == null || _client == null)
        {
            throw new InvalidOperationException("Not connected to server. Call Connect() first.");
        }

        try
        {
            byte[] message;

            if (type != MessageType.Command)
            {
                message = Protocol.CreateMessage(type, _secretPassword, content);
            }
            else
            {
                if (commandType == null)
                    throw new ArgumentNullException(nameof(commandType), "CommandType is required for Command messages");
                message = Protocol.CreateMessage(type, commandType.Value, _secretPassword, content);
            }

            _stream.Write(message, 0, message.Length);

            return ReceiveResponse();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error sending message: {e.Message}");
            throw;
        }
    }

    private (object Type, string Content) ReceiveResponse()
    {
        if (_stream == null)
        {
            throw new InvalidOperationException("Not connected to server.");
        }

        byte[] headerBuffer = new byte[Protocol.HeaderSize];
        int bytesRead = _stream.Read(headerBuffer, 0, headerBuffer.Length);

        if (bytesRead != Protocol.HeaderSize)
        {
            throw new Exception($"Failed to receive a complete header. Bytes read: {bytesRead}");
        }

        int messageLength = Protocol.GetMessageLength(headerBuffer);
        byte[] messageBuffer = new byte[messageLength];
        Array.Copy(headerBuffer, messageBuffer, Protocol.HeaderSize);
        bytesRead = _stream.Read(messageBuffer, Protocol.HeaderSize, messageLength - Protocol.HeaderSize);

        if (bytesRead != messageLength - Protocol.HeaderSize)
        {
            throw new Exception($"Failed to read complete message. Expected {messageLength - Protocol.HeaderSize} bytes, but read {bytesRead}");
        }

        var (resType, resContent) = Protocol.ParseMessage(messageBuffer);
        return (resType, resContent);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _stream?.Dispose();
                _client?.Dispose();
            }

            disposed = true;
        }
    }

    ~ClientServer()
    {
        Dispose(false);
    }
}