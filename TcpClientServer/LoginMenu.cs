using TcpProtocol;

namespace TcpClientServer;

public class LoginMenu(IUserInterface userInterface, IUserInteraction userInteraction)
{
    private readonly IUserInterface _userInterface = userInterface;
    private readonly IUserInteraction _userInteraction = userInteraction;
    private readonly string[] _menuOptions = ["1 -> Log In", "2 -> Create Account", "3 -> Exit", "Your choice -> "];

    public MessageType Show()
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
        _userInterface.Write(MenuOptions());
        do
        {
            (bool valid, int choice) = ValidateInput(_userInteraction.ReadLine());
            if (valid)
            {
                return choice;
            }

            _userInterface.Clear();
            _userInterface.Write(
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
