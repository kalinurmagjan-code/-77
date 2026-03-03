using System;
using System.Collections.Generic;

#region ================= COMMAND PATTERN =================

public interface ICommand
{
    void Execute();
    void Undo();
}

// ===== Devices =====
public class Light
{
    public void On() => Console.WriteLine("Light ON");
    public void Off() => Console.WriteLine("Light OFF");
}

public class TV
{
    public void On() => Console.WriteLine("TV ON");
    public void Off() => Console.WriteLine("TV OFF");
}

public class AirConditioner
{
    private int temperature = 24;

    public void IncreaseTemp()
    {
        temperature++;
        Console.WriteLine($"AC Temperature: {temperature}");
    }

    public void DecreaseTemp()
    {
        temperature--;
        Console.WriteLine($"AC Temperature: {temperature}");
    }
}

// ===== Commands =====
public class LightOnCommand : ICommand
{
    private Light light;
    public LightOnCommand(Light l) => light = l;
    public void Execute() => light.On();
    public void Undo() => light.Off();
}

public class TVOnCommand : ICommand
{
    private TV tv;
    public TVOnCommand(TV t) => tv = t;
    public void Execute() => tv.On();
    public void Undo() => tv.Off();
}

public class IncreaseTempCommand : ICommand
{
    private AirConditioner ac;
    public IncreaseTempCommand(AirConditioner a) => ac = a;
    public void Execute() => ac.IncreaseTemp();
    public void Undo() => ac.DecreaseTemp();
}

// ===== Macro Command =====
public class MacroCommand : ICommand
{
    private List<ICommand> commands;

    public MacroCommand(List<ICommand> cmds)
    {
        commands = cmds;
    }

    public void Execute()
    {
        foreach (var cmd in commands)
            cmd.Execute();
    }

    public void Undo()
    {
        for (int i = commands.Count - 1; i >= 0; i--)
            commands[i].Undo();
    }
}

// ===== Remote Control =====
public class RemoteControl
{
    private Dictionary<int, ICommand> slots = new();
    private Stack<ICommand> undoStack = new();
    private Stack<ICommand> redoStack = new();

    public void SetCommand(int slot, ICommand command)
    {
        slots[slot] = command;
    }

    public void PressButton(int slot)
    {
        if (!slots.ContainsKey(slot))
        {
            Console.WriteLine("No command assigned to this slot!");
            return;
        }

        var command = slots[slot];
        command.Execute();
        undoStack.Push(command);
        redoStack.Clear();
    }

    public void Undo()
    {
        if (undoStack.Count == 0)
        {
            Console.WriteLine("Nothing to undo!");
            return;
        }

        var cmd = undoStack.Pop();
        cmd.Undo();
        redoStack.Push(cmd);
    }

    public void Redo()
    {
        if (redoStack.Count == 0)
        {
            Console.WriteLine("Nothing to redo!");
            return;
        }

        var cmd = redoStack.Pop();
        cmd.Execute();
        undoStack.Push(cmd);
    }
}

#endregion

#region ================= TEMPLATE METHOD PATTERN =================

public abstract class ReportGenerator
{
    public void GenerateReport()
    {
        Log("Start generation");
        FetchData();
        FormatData();
        CreateHeader();
        CreateBody();
        if (CustomerWantsSave())
            Save();
        Log("Report finished");
    }

    protected void FetchData() => Console.WriteLine("Fetching data...");
    protected abstract void FormatData();
    protected abstract void CreateHeader();
    protected abstract void CreateBody();

    protected virtual bool CustomerWantsSave()
    {
        Console.Write("Save report? (y/n): ");
        var input = Console.ReadLine()?.ToLower();
        return input == "y";
    }

    protected virtual void Save()
    {
        Console.WriteLine("Saving report...");
    }

    protected void Log(string message)
    {
        Console.WriteLine($"[LOG] {message}");
    }
}

public class PdfReport : ReportGenerator
{
    protected override void FormatData() => Console.WriteLine("Formatting for PDF...");
    protected override void CreateHeader() => Console.WriteLine("PDF Header");
    protected override void CreateBody() => Console.WriteLine("PDF Body");
}

public class ExcelReport : ReportGenerator
{
    protected override void FormatData() => Console.WriteLine("Formatting for Excel...");
    protected override void CreateHeader() => Console.WriteLine("Excel Header");
    protected override void CreateBody() => Console.WriteLine("Excel Body");

    protected override void Save()
    {
        Console.WriteLine("Saving as .xlsx file");
    }
}

public class HtmlReport : ReportGenerator
{
    protected override void FormatData() => Console.WriteLine("Formatting for HTML...");
    protected override void CreateHeader() => Console.WriteLine("<h1>HTML Header</h1>");
    protected override void CreateBody() => Console.WriteLine("<p>HTML Body</p>");
}

#endregion

#region ================= MEDIATOR PATTERN =================

public interface IMediator
{
    void SendMessage(string channel, string message, IUser sender);
    void RegisterUser(string channel, IUser user);
    void RemoveUser(string channel, IUser user);
}

public interface IUser
{
    string Name { get; }
    void Receive(string message);
}

public class ChatMediator : IMediator
{
    private Dictionary<string, List<IUser>> channels = new();
    private HashSet<string> blockedUsers = new();

    public void RegisterUser(string channel, IUser user)
    {
        if (!channels.ContainsKey(channel))
            channels[channel] = new List<IUser>();

        channels[channel].Add(user);
        Broadcast(channel, $"{user.Name} joined the channel");
    }

    public void RemoveUser(string channel, IUser user)
    {
        if (channels.ContainsKey(channel))
        {
            channels[channel].Remove(user);
            Broadcast(channel, $"{user.Name} left the channel");
        }
    }

    public void SendMessage(string channel, string message, IUser sender)
    {
        if (blockedUsers.Contains(sender.Name))
        {
            sender.Receive("You are blocked!");
            return;
        }

        if (!channels.ContainsKey(channel))
        {
            sender.Receive("Channel does not exist!");
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

#endregion

// ================= MAIN =================

class Program
{
    static void Main()
    {
        Console.WriteLine("===== COMMAND =====");
        var remote = new RemoteControl();

        var light = new Light();
        var tv = new TV();
        var ac = new AirConditioner();

        remote.SetCommand(1, new LightOnCommand(light));
        remote.SetCommand(2, new TVOnCommand(tv));
        remote.SetCommand(3, new IncreaseTempCommand(ac));

        remote.PressButton(1);
        remote.PressButton(2);
        remote.PressButton(3);

        remote.Undo();
        remote.Redo();

        Console.WriteLine("\n===== TEMPLATE METHOD =====");
        ReportGenerator pdf = new PdfReport();
        pdf.GenerateReport();

        Console.WriteLine("\n===== MEDIATOR =====");
        var mediator = new ChatMediator();

        var user1 = new User("Askar", mediator);
        var user2 = new User("Ali", mediator);

        user1.Join("General");
        user2.Join("General");

        user1.Send("Hello!");
        mediator.BlockUser("Ali");
        user2.Send("Why no one hears me?");
    }
}