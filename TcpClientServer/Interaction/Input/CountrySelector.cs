namespace TcpClientServer;

public class CountrySelector(IUserInterface userInterface, IUserInteraction userInteraction)
{
    private readonly IUserInterface _userInterface = userInterface;
    private readonly IUserInteraction _userInteraction = userInteraction;

    public (int id, int code) Get(List<Country> countries)
    {
        char[] pointer = new char[countries.Count];
        pointer[^1] = '>';
        int pointerIndex = pointer.Length - 1;

        do
        {
            _userInterface.Clear();

            for (int i = 0; i < (pointerIndex < 17 ?
                                34 : pointerIndex + 17 < pointer.Length ?
                                pointerIndex + 17 : pointer.Length); i++)
            {
                Console.WriteLine($"{pointer[i]} {countries[i].Name}");
            }

            char input = char.ToLowerInvariant(_userInteraction.ReadKey(true));

            if (input == 'q')
            {
                return (0, 0);
            }

            if (input == 'e')
            {
                return (countries[pointerIndex].Id, countries[pointerIndex].Code);
            }

            if (input == 'k' && pointerIndex != 0)
            {
                pointer[pointerIndex] = '\0';
                pointer[--pointerIndex] = '>';
            }

            if (input == 'j' && pointerIndex != pointer.Length - 1)
            {
                pointer[pointerIndex] = '\0';
                pointer[++pointerIndex] = '>';
            }

        } while (true);
    }
}
