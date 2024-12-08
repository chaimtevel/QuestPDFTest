using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDFTest.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace QuestPDFTest.Extensions;

internal static class PdfGenerators
    {
        public static QuestPDF.Fluent.Document GetHelloWorld()
        {
            return QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                        .Text("Hello PDF!")
                        .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(20);

                            x.Item().Text(Placeholders.LoremIpsum());
                            x.Item().Image(Placeholders.Image(200, 100));
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
        }


        public static QuestPDF.Fluent.Document GetResourcesDoc()
        {

            var resources = ResourcesGenerator.GetResourcesPDFModels();

            // code in your main method
            return QuestPDF.Fluent.Document.Create(container =>
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


    }
