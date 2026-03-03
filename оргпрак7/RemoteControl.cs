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
            Console.WriteLine("Бұл батырмаға команда бекітілмеген!");
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
            Console.WriteLine("Қайтатын әрекет жоқ!");
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
            Console.WriteLine("Қайта орындайтын әрекет жоқ!");
            return;
        }

        var cmd = redoStack.Pop();
        cmd.Execute();
        undoStack.Push(cmd);
    }
}