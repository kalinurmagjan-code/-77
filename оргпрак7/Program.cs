class Program
{
    static void Main()
    {
        Console.WriteLine("===== КОМАНДА ПАТТЕРНІ =====");
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

        Console.WriteLine("\n===== ШАБЛОНДЫ ӘДІС =====");
        ReportGenerator pdf = new PdfReport();
        pdf.GenerateReport();

        Console.WriteLine("\n===== ПОСРЕДНИК ПАТТЕРНІ =====");
        var mediator = new ChatMediator();

        var user1 = new User("Асқар", mediator);
        var user2 = new User("Әлі", mediator);

        user1.Join("Жалпы");
        user2.Join("Жалпы");

        user1.Send("Сәлем!");
        mediator.BlockUser("Әлі");
        user2.Send("Неге ешкім жауап бермейді?");
    }
}