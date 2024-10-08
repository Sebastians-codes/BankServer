namespace TcpClientServer;

public class Input(Name name, Email email, Ssn ssn, Pin pin)
{
    private readonly Ssn _ssn = ssn;
    private readonly Pin _pin = pin;
    private readonly Name _name = name;
    private readonly Email _email = email;
}
