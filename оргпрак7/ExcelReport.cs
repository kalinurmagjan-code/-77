public class ExcelReport : ReportGenerator
{
    protected override void FormatData() => Console.WriteLine("Excel үшін форматтау...");
    protected override void CreateHeader() => Console.WriteLine("Excel Тақырыбы");
    protected override void CreateBody() => Console.WriteLine("Excel Мазмұны");

    protected override void Save()
    {
        Console.WriteLine(".xlsx файлы ретінде сақталды");
    }
}