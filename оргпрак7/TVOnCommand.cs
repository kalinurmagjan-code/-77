public class TVOnCommand : ICommand
{
    private TV tv;
    public TVOnCommand(TV t) => tv = t;

    public void Execute() => tv.On();
    public void Undo() => tv.Off();
}