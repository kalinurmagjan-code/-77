public abstract class ReportGenerator
{
    public void GenerateReport()
    {
        Log("Есеп дайындау басталды");
        FetchData();
        FormatData();
        CreateHeader();
        CreateBody();
        if (CustomerWantsSave())
            Save();
        Log("Есеп дайындау аяқталды");
    }

    protected void FetchData() => Console.WriteLine("Деректер жүктелуде...");
    protected abstract void FormatData();
    protected abstract void CreateHeader();
    protected abstract void CreateBody();

    protected virtual bool CustomerWantsSave()
    {
        Console.Write("Есепті сақтау керек пе? (и/ж): ");
        var input = Console.ReadLine()?.ToLower();
        return input == "и";
    }

    protected virtual void Save()
    {
        Console.WriteLine("Есеп сақталды...");
    }

    protected void Log(string message)
    {
        Console.WriteLine($"[ЛОГ] {message}");
    }
}