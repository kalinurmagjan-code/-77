public class PdfReport : ReportGenerator
{
    protected override void FormatData() => Console.WriteLine("PDF үшін форматтау...");
    protected override void CreateHeader() => Console.WriteLine("PDF Құжаттың тақырыбы");
    protected override void CreateBody() => Console.WriteLine("PDF Құжаттың мазмұны");
}