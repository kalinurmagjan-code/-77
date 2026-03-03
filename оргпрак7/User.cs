public class User : IUser
{
    public string Name { get; }
    private IMediator mediator;
    private string currentChannel;

    public User(string name, IMediator med)
    {
        Name = name;
        mediator = med;
    }

    public void Join(string channel)
    {
        currentChannel = channel;
        mediator.RegisterUser(channel, this);
    }

    public void Leave()
    {
        mediator.RemoveUser(currentChannel, this);
    }

    public void Send(string message)
    {
        mediator.SendMessage(currentChannel, message, this);
    }

    public void Receive(string message)
    {
        Console.WriteLine($"[{Name}] {message}");
    }
}