using System.Net;
using System.Net.Sockets;
using TcpProtocol;
using TcpServer;

public class Server(string serverIp, int port, string secretPassword) : IDisposable
{
    private readonly string _secretPassword = secretPassword;
    private TcpListener? _server;
    private readonly IPAddress _serverIp = IPAddress.Parse(serverIp);
    private readonly int _port = port;
    private bool _isRunning;
    private bool _disposed = false;

    public void Start()
    {
        try
        {
            _server = new TcpListener(_serverIp, _port);
            _server.Start();
            _isRunning = true;
            Console.WriteLine($"Server is running on {_serverIp}:{_port}. Waiting for connections...");

            while (_isRunning)
            {
                using TcpClient client = _server.AcceptTcpClient();
                Console.WriteLine("Client connected!");
                HandleClient(client);
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine($"SocketException: {e.Message}");
        }
        finally
        {
            Stop();
        }
    }

    private void HandleClient(TcpClient client)
    {
        using NetworkStream stream = client.GetStream();

        while (true)
        {
            try
            {
                byte[] headerBuffer = new byte[Protocol.HeaderSize];
                int bytesRead = stream.Read(headerBuffer, 0, Protocol.HeaderSize);

                if (bytesRead == 0)
                {
                    Console.WriteLine("Client disconnected.");
                    break;
                }

                if (bytesRead != Protocol.HeaderSize)
                {
                    throw new Exception($"Failed to receive complete header. Bytes read: {bytesRead}");
                }

                int messageLength = Protocol.GetMessageLength(headerBuffer);
                byte[] messageBuffer = new byte[messageLength];
                Array.Copy(headerBuffer, messageBuffer, Protocol.HeaderSize);
                bytesRead = stream.Read(messageBuffer, Protocol.HeaderSize, messageLength - Protocol.HeaderSize);

                if (bytesRead != messageLength - Protocol.HeaderSize)
                {
                    throw new Exception($"Failed to read the full message. Expected {messageLength - Protocol.HeaderSize} bytes, but read {bytesRead}");
                }

                var (type, content) = Protocol.ParseMessage(messageBuffer);
                HandleMessage(type, content, stream);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error handling client: {e.Message}");
                Console.WriteLine(e.StackTrace);
                break;
            }
        }
    }

    private void HandleMessage(object type, string content, NetworkStream stream)
    {
        switch (type)
        {
            case MessageType messageType:
                HandleMessageType(messageType, content, stream);
                break;
            case CommandType commandType:
                HandleCommandType(commandType, content, stream);
                break;
            default:
                Console.WriteLine($"Unknown type: {type}");
                break;
        }

        Console.WriteLine($"Received Type:{type} Content:{content}");
    }

    private void HandleMessageType(MessageType type, string content, NetworkStream stream)
    {
        (MessageType Type, string Text) response = type switch
        {
            MessageType.Login => Login.Validate(content),
            MessageType.CreateLogin => Login.CreateAccount(content),
            MessageType.Country => Data.GetCountries(),
            MessageType.City => Data.GetCities(int.Parse(content)),
            _ => (MessageType.Err, $"Unhandled MessageType: {type}")
        };

        byte[] responseMessage = Protocol.CreateMessage(response.Type, _secretPassword, response.Text);
        stream.Write(responseMessage, 0, responseMessage.Length);
        Console.WriteLine($"Sent Response: {response.Text}");
    }

    private void HandleCommandType(CommandType type, string content, NetworkStream stream)
    {
        (MessageType Type, string Text) response = type switch
        {
            CommandType.Transaction => AccountCommands.MakeTransaction(content)
        };

        byte[] commandMessage = Protocol.CreateMessage(response.Type, _secretPassword, response.Text);
        stream.Write(commandMessage, 0, commandMessage.Length);
        Console.WriteLine($"Sent Response: {response.Text}");
    }

    public void Stop()
    {
        _isRunning = false;
        _server?.Stop();
        Console.WriteLine("Server has stopped.");
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                Stop();
            }
            _disposed = true;
        }
    }

    ~Server()
    {
        Dispose(false);
    }
}