namespace TcpClientServer;

public class Email(IUserInterface userInterface, IUserInteraction userInteraction)
{
    private readonly IUserInterface _userInterface = userInterface;
    private readonly IUserInteraction _userInteraction = userInteraction;

    public string Get()
    {
        do
        {
            _userInterface.Write("Exit to quit\nEnter a email address -> ");
            string input = _userInteraction.ReadLine().ToLowerInvariant();

            if (input == "exit")
            {
                return "q";
            }

            if (ValidateEmail(input))
            {
                return input;
            }

            _userInterface.Clear();
            _userInterface.WriteLine("Invalid format try again.");
        } while (true);
    }

    private bool ValidateEmail(string input)
    {
        if (string.IsNullOrWhiteSpace(input) || input.Contains(' '))
        {
            return false;
        }

        string[] parts = input.Split('@');

        if (parts.Length != 2)
        {
            return false;
        }

        string personal = parts[0];
        string domain = parts[1];

        int dotIndex = domain.IndexOf('.');

        if (dotIndex == -1 || dotIndex == domain.Length - 1 || dotIndex == 0)
        {
            return false;
        }

        if (personal[0] == '.' || personal[^1] == '.')
        {
            return false;
        }

        return true;
    }
}