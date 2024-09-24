using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDFTest.Generators;

var start = DateTime.UtcNow;

QuestPDF.Settings.License = LicenseType.Community;

var paths = QuestPDF.Settings.FontDiscoveryPaths;
paths.Clear();

paths.Add(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LatoFont"));

var emptyDirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "empty");
if (Directory.Exists(emptyDirPath) == false)
{
    Directory.CreateDirectory(emptyDirPath);
}
paths.Add(emptyDirPath);


var resources = ResourcesGenerator.GetResourcesPDFModels();

// code in your main method
var doc = QuestPDF.Fluent.Document.Create(container =>
{
    container.Page(page =>
    {
        page.Size(PageSizes.A4);
        page.Margin(2, Unit.Centimetre);
        page.PageColor(Colors.White);
        page.DefaultTextStyle(x => x.FontSize(12));

        page.Header()
            .Text("Accounts")
            .SemiBold().FontSize(25).FontColor(Colors.Blue.Medium);

        page.Content()
            .Table(tbl =>
            {
                tbl.ColumnsDefinition(col =>
                {
                    col.RelativeColumn();
                    col.RelativeColumn();
                    col.RelativeColumn();
                    col.RelativeColumn();
                    col.RelativeColumn();
                    col.RelativeColumn();
                });

                uint rowCounter = 1;

                // header
                tbl.Cell().Row(rowCounter).Column(1).Element(HeaderBlock).Text("Type");
                tbl.Cell().Row(rowCounter).Column(2).Element(HeaderBlock).Text("Bank Name");
                tbl.Cell().Row(rowCounter).Column(3).Element(HeaderBlock).Text("Bank Address");
                tbl.Cell().Row(rowCounter).Column(4).Element(HeaderBlock).Text("Account Number");
                tbl.Cell().Row(rowCounter).Column(5).Element(HeaderBlock).Text("Current Value");
                tbl.Cell().Row(rowCounter).Column(6).Element(HeaderBlock).Text("Names on account");

                foreach (var resource in resources)
                {
                    rowCounter++;

                    tbl.Cell().Row(rowCounter).Column(1).Element(ContentBlock).Text(resource.AccountTypeDisplay);
                    tbl.Cell().Row(rowCounter).Column(2).Element(ContentBlock).Text(resource.InstitutionName);
                    tbl.Cell().Row(rowCounter).Column(3).Element(ContentBlock);
                    tbl.Cell().Row(rowCounter).Column(4).Element(ContentBlock).Text(resource.AccountNumber);
                    tbl.Cell().Row(rowCounter).Column(5).Element(ContentBlock).Text(resource.CurrentValue.ToString("$#,##0.00"));
                    tbl.Cell().Row(rowCounter).Column(6).Element(ContentBlock);
                }
            });

        page.Footer()
            .AlignCenter()
            .Text(x =>
            {
                x.Span("Page ");
                x.CurrentPageNumber();
            });
    });

    container.Page(page =>
    {
        page.Size(PageSizes.A4);
        page.Margin(2, Unit.Centimetre);
        page.PageColor(Colors.White);
        page.DefaultTextStyle(x => x.FontSize(12));

        page.Header()
            .Text("Accounts")
            .SemiBold().FontSize(25).FontColor(Colors.Blue.Medium);

        page.Content()
            .Table(tbl =>
            {
                tbl.ColumnsDefinition(col =>
                {
                    col.RelativeColumn();
                    col.RelativeColumn();
                    col.RelativeColumn();
                    col.RelativeColumn();
                    col.RelativeColumn();
                    col.RelativeColumn();
                });

                uint rowCounter = 1;

                // header
                tbl.Cell().Row(rowCounter).Column(1).Element(HeaderBlock).Text("Type");
                tbl.Cell().Row(rowCounter).Column(2).Element(HeaderBlock).Text("Bank Name");
                tbl.Cell().Row(rowCounter).Column(3).Element(HeaderBlock).Text("Bank Address");
                tbl.Cell().Row(rowCounter).Column(4).Element(HeaderBlock).Text("Account Number");
                tbl.Cell().Row(rowCounter).Column(5).Element(HeaderBlock).Text("Current Value");
                tbl.Cell().Row(rowCounter).Column(6).Element(HeaderBlock).Text("Names on account");

                foreach (var resource in resources)
                {
                    rowCounter++;

                    tbl.Cell().Row(rowCounter).Column(1).Element(ContentBlock).Text(resource.AccountTypeDisplay);
                    tbl.Cell().Row(rowCounter).Column(2).Element(ContentBlock).Text(resource.InstitutionName);
                    tbl.Cell().Row(rowCounter).Column(3).Element(ContentBlock);
                    tbl.Cell().Row(rowCounter).Column(4).Element(ContentBlock).Text(resource.AccountNumber);
                    tbl.Cell().Row(rowCounter).Column(5).Element(ContentBlock).Text(resource.CurrentValue.ToString("$#,##0.00"));
                    tbl.Cell().Row(rowCounter).Column(6).Element(ContentBlock);
                }
            });

        page.Footer()
            .AlignCenter()
            .Text(x =>
            {
                x.Span("Page ");
                x.CurrentPageNumber();
            });
    });
});


// SearchFontFiles(QuestPDF.Settings.FontDiscoveryPaths);

// doc.GeneratePdf($"{Guid.NewGuid():n}.pdf");
doc.GeneratePdf();

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

static IContainer HeaderBlock(IContainer container)
{
    return container
        .Border(1)
        .Background(Colors.Grey.Lighten1)
        .ShowOnce()
        .MinWidth(50)
        .MinHeight(50)
        .AlignCenter()
        .AlignMiddle();
}


static IContainer ContentBlock(IContainer container)
{
    return container
        .Border(1)
        .Background(Colors.Grey.Lighten5)
        .ShowOnce()
        .MinWidth(50)
        .MinHeight(50)
        .AlignCenter()
        .AlignMiddle();
}
