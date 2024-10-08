namespace TcpClientServer;

public class Street(IUserInterface userInterface, IUserInteraction userInteraction)
{
    private readonly IUserInterface _userInterface = userInterface;
    private readonly IUserInteraction _userInteraction = userInteraction;

    public string Get()
    {
        do
        {
            _userInterface.Write("Exit to quit\nEnter your street -> ");
            string input = _userInteraction.ReadLine();

            if (input.ToLowerInvariant() == "exit")
            {
                return "q";
            }

            if (string.IsNullOrWhiteSpace(input) || input.Length < 4)
            {
                _userInterface.Clear();
                _userInterface.WriteLine("Invalid input try again.");
                continue;
            }

            return input;

        } while (true);
    }
}
