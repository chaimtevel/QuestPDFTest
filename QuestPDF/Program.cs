using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDFTest.Extensions;

var start = DateTime.UtcNow;

QuestPDF.Settings.License = LicenseType.Community;

QuestPDF.Settings.FontDiscoveryPaths.Clear();
QuestPDF.Settings.FontDiscoveryPaths.Add(@"C:\Users\chaim\source\projects\aidace\TestProjects\QuestPDF\bin\Debug\net8.0\LatoFont");


QuestPDF.Fluent.Document doc = PdfGenerators.GetHelloWorld();

var helloWorldPath = @"C:\Users\chaim\Desktop\dev\aidace\doc-gen-forms\hello-world.pdf";
var helloWorldQpdfPath = @"C:\Users\chaim\Desktop\dev\aidace\doc-gen-forms\hello-world-qpdf.pdf";

helloWorldPath = "/berel/hello-world.pdf";
helloWorldQpdfPath = "/berel/hello-world-qpdf.pdf";

doc.GeneratePdf(helloWorldPath);

var docOperation = DocumentOperation.LoadFile(helloWorldPath);
docOperation.Save(helloWorldQpdfPath);

// SearchFontFiles(QuestPDF.Settings.FontDiscoveryPaths);

// doc.GeneratePdf($"{Guid.NewGuid():n}.pdf");


// doc.ShowInCompanion();

var end = DateTime.UtcNow;

Console.WriteLine($"Ran for {end.Subtract(start).TotalMilliseconds} ms");




ICollection<string> SearchFontFiles(ICollection<string> col)
{
    const int maxFilesToScan = 100_000;

    var applicationFiles = col
        .Where(Directory.Exists)
        .Select(path => Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories))
        .SelectMany(file => file)
        .Take(maxFilesToScan)
        .ToList();

    if (applicationFiles.Count == maxFilesToScan)
        throw new InvalidOperationException($"The library has reached the limit of {maxFilesToScan} files to scan for font files. Please adjust the {nameof(Settings.FontDiscoveryPaths)} collection to include only the necessary directories. The reason of this exception is to prevent scanning too many files and avoid performance issues on the application startup.");

    var supportedFontExtensions = new[] { ".ttf", ".otf", ".ttc", ".pfb" };

    return applicationFiles
        .Where(x => supportedFontExtensions.Contains(Path.GetExtension(x).ToLowerInvariant()))
        .ToList();
}
