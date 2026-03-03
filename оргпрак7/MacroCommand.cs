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