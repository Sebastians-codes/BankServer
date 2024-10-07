using System.Text;

namespace TcpProtocol;

public enum MessageType : byte
{
    Login = 1,
    CreateLogin = 2,
    Command = 3,
    Err = 4,
    Ok = 5,
    Country = 6,
    City = 7
}

public enum CommandType : byte
{
    Transaction = 1,
    Transfer = 2,
    CreateAccount = 3,
    DeleteAccount = 4
}

public static class Protocol
{
    public const int HeaderSize = 7; // 1 byte for type, 2 bytes for password length, 4 bytes for content length
    private const string SecretPassword = "BIGFUCKINGSECRET!"; // Loaded from remote location

    public static byte[] CreateMessage(MessageType type, string password, string content)
    {
        string encryptedContent = Encrypt(content);

        if (password != SecretPassword)
        {
            throw new ArgumentException("Invalid password");
        }

        var contentBytes = Encoding.UTF8.GetBytes(encryptedContent);
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var message = new byte[HeaderSize + contentBytes.Length + passwordBytes.Length + 1];

        message[0] = (byte)type;
        BitConverter.GetBytes(passwordBytes.Length).CopyTo(message, 1);
        BitConverter.GetBytes(contentBytes.Length).CopyTo(message, 3);

        Array.Copy(passwordBytes, 0, message, HeaderSize, passwordBytes.Length);
        Array.Copy(contentBytes, 0, message, HeaderSize + passwordBytes.Length, contentBytes.Length);

        return message;
    }

    public static byte[] CreateMessage(MessageType type, CommandType command, string password, string content)
    {
        string encryptedContent = Encrypt(content);


        if (password != SecretPassword)
        {
            throw new ArgumentException("Invalid password");
        }

        var contentBytes = Encoding.UTF8.GetBytes(encryptedContent);
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] message = new byte[HeaderSize + contentBytes.Length + passwordBytes.Length + 1];

        message[0] = (byte)type;
        BitConverter.GetBytes(passwordBytes.Length).CopyTo(message, 1);
        BitConverter.GetBytes(contentBytes.Length).CopyTo(message, 3);
        message[^1] = (byte)command;

        Array.Copy(passwordBytes, 0, message, HeaderSize, passwordBytes.Length);
        Array.Copy(contentBytes, 0, message, HeaderSize + passwordBytes.Length, contentBytes.Length);

        return message;
    }

    public static (object type, string content) ParseMessage(byte[] message)
    {
        if (message.Length < HeaderSize)
        {
            throw new ArgumentException("Message is too short");
        }

        object type;
        if ((MessageType)message[0] == MessageType.Command)
        {
            type = (CommandType)message[^1];
        }
        else
        {
            type = (MessageType)message[0];
        }

        short passwordLength = BitConverter.ToInt16(message, 1);
        int contentLength = BitConverter.ToInt32(message, 3);

        if (message.Length != HeaderSize + passwordLength + contentLength + 1)
        {
            throw new ArgumentException("Message length mismatch");
        }

        string password = Encoding.UTF8.GetString(message, HeaderSize, passwordLength);

        if (password != SecretPassword)
        {
            throw new ArgumentException("Faulty password");
        }

        string content = Encoding.UTF8.GetString(message, HeaderSize + passwordLength, contentLength);
        string decodedContent = Decrypt(content);

        return (type, decodedContent);
    }

    public static int GetMessageLength(byte[] headerBuffer)
    {
        if (headerBuffer.Length < HeaderSize)
        {
            throw new ArgumentException("Header Buffer is too short");
        }

        short passwordLength = BitConverter.ToInt16(headerBuffer, 1);
        int contentLength = BitConverter.ToInt32(headerBuffer, 3);

        return HeaderSize + passwordLength + contentLength + 1;
    }

    private static string Encrypt(string content)
    {
        StringBuilder sb = new();
        int contentLength = content.Length;
        for (int i = 0; i < contentLength; i++)
        {
            sb.Append((char)(content[i] + contentLength));
        }
        return sb.ToString();
    }

    private static string Decrypt(string content)
    {
        StringBuilder sb = new();
        int contentLength = content.Length;
        for (int i = 0; i < contentLength; i++)
        {
            sb.Append((char)(content[i] - contentLength));
        }
        return sb.ToString();
    }
}

