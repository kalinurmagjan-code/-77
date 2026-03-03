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