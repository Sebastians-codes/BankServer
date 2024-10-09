using System.Text.Json;
using TcpClientServer.Server;
using TcpProtocol;

namespace TcpClientServer;

public class LoginMenu(
    IUserInterface userInterface,
    IUserInteraction userInteraction,
    Input input,
    ClientServer server
)
{
    private readonly CustomerFactory _customerFactory = new(input, server);
    private readonly string[] _menuOptions =
        ["1 -> Log In", "2 -> Create Account", "3 -> Exit", "Your choice -> "];

    public Customer? Show()
    {
        MessageType type = ShowMenu();

        if (type == MessageType.Login)
        {
            string ssn = input.GetSsn();
            string pin = input.GetPin();

            LoginCredentials customer = new(ssn, pin);
            (object Type, string Response) = server.SendMessage(type, JsonSerializer.Serialize(customer));

            switch (Type)
            {
                case MessageType.Ok:
                    return JsonSerializer.Deserialize<Customer>(Response)!;
                case MessageType.Err:
                    Console.WriteLine("Invalid Credentials");
                    break;
            }
        }
        else if (type == MessageType.CreateLogin)
        {
            (object Type, string Response) =
                server.SendMessage(type, JsonSerializer.Serialize(_customerFactory.Make()));

            switch (Type)
            {
                case MessageType.Ok:
                    Console.WriteLine("Account created");
                    break;
                case MessageType.Err:
                    Console.WriteLine("Account not created");
                    break;
            }
        }

        return null;
    }

    private MessageType ShowMenu()
    {
        switch (GetMenuChoice())
        {
            case 1:
                return MessageType.Login;
            case 2:
                return MessageType.CreateLogin;
            case 3:
                Environment.Exit(0);
                break;
            default:
                break;
        }
        return MessageType.Err;
    }

    private int GetMenuChoice()
    {
        userInterface.Write(MenuOptions());
        do
        {
            (bool valid, int choice) = ValidateInput(userInteraction.ReadLine());
            if (valid)
            {
                return choice;
            }

            userInterface.Clear();
            userInterface.Write(
                "Invalid option you options are\n" + string.Join(Environment.NewLine, _menuOptions)
            );

        } while (true);
    }


    private string MenuOptions() =>
        string.Join(Environment.NewLine, _menuOptions);

    private (bool valid, int choice) ValidateInput(string input)
    {
        if (int.TryParse(input, out int menuChoice) && menuChoice > 0 && menuChoice < _menuOptions.Length)
        {
            return (true, menuChoice);
        }

        return (false, 0);
    }
}
