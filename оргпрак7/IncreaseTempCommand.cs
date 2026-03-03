public class IncreaseTempCommand : ICommand
{
    private AirConditioner ac;
    public IncreaseTempCommand(AirConditioner a) => ac = a;

    public void Execute() => ac.IncreaseTemp();
    public void Undo() => ac.DecreaseTemp();
}