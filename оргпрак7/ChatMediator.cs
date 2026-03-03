public class ChatMediator : IMediator
{
    private Dictionary<string, List<IUser>> channels = new();
    private HashSet<string> blockedUsers = new();

    public void RegisterUser(string channel, IUser user)
    {
        if (!channels.ContainsKey(channel))
            channels[channel] = new List<IUser>();

        channels[channel].Add(user);
        Broadcast(channel, $"{user.Name} арнаға қосылды");
    }

    public void RemoveUser(string channel, IUser user)
    {
        if (channels.ContainsKey(channel))
        {
            channels[channel].Remove(user);
            Broadcast(channel, $"{user.Name} арнадан шықты");
        }
    }

    public void SendMessage(string channel, string message, IUser sender)
    {
        if (blockedUsers.Contains(sender.Name))
        {
            sender.Receive("Сіз бұғатталғансыз!");
            return;
        }

        if (!channels.ContainsKey(channel))
        {
            sender.Receive("Мұндай арна жоқ!");
            return;
        }

        foreach (var user in channels[channel])
        {
            if (user != sender)
                user.Receive($"{sender.Name}: {message}");
        }
    }

    private void Broadcast(string channel, string message)
    {
        foreach (var user in channels[channel])
            user.Receive(message);
    }

    public void BlockUser(string username)
    {
        blockedUsers.Add(username);
    }
}