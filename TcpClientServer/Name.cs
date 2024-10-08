namespace TcpClientServer;

public class Name(IUserInterface userInterface, IUserInteraction userInteraction)
{
    private readonly IUserInterface _userInterface = userInterface;
    private readonly IUserInteraction _userInteraction = userInteraction;

    public string Get(string message)
    {
        do
        {
            _userInterface.Write(message);
            string input = _userInteraction.ReadLine();

            if (string.IsNullOrWhiteSpace(input) || input.Length < 2)
            {
                _userInterface.Clear();
                _userInterface.WriteLine("Must be at least 2 chars long.");
                continue;
            }

            return input;

        } while (true);
    }
}
