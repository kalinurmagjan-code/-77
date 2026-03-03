public class HtmlReport : ReportGenerator
{
    protected override void FormatData() => Console.WriteLine("HTML үшін форматтау...");
    protected override void CreateHeader() => Console.WriteLine("<h1>HTML Тақырып</h1>");
    protected override void CreateBody() => Console.WriteLine("<p>HTML Мәтін</p>");
}