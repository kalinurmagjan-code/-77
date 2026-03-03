public interface IUser
{
    string Name { get; }
    void Receive(string message);
}