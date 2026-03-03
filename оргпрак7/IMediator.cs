public interface IMediator
{
    void SendMessage(string channel, string message, IUser sender);
    void RegisterUser(string channel, IUser user);
    void RemoveUser(string channel, IUser user);
}