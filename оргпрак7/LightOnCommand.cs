public class LightOnCommand : ICommand
{
    private Light light;
    public LightOnCommand(Light l) => light = l;

    public void Execute() => light.On();
    public void Undo() => light.Off();
}