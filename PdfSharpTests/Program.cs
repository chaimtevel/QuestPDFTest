
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.AcroForms;
using PdfSharp.Pdf.Advanced;
using PdfSharp.Pdf.Annotations;
using PdfSharp.Drawing;
using System.Diagnostics;


string path = @"C:\Users\chaim\source\projects\aidace\aidace\backend\MicroServices\DocService\bin\Debug\net8.0\system-mount\pdf-forms\NJ_Application.pdf";

string savePath = @"C:\Users\chaim\Desktop\dev\aidace\doc-gen-forms\test.pdf";



//using PdfDocument pdf = new();
//PdfPage page1 = pdf.AddPage();

//double left = 50;
//double right = 200;
//double bottom = 750;
//double top = 725;

//PdfArray rect = new PdfArray(pdf);
//rect.Elements.Add(new PdfReal(left));
//rect.Elements.Add(new PdfReal(bottom));
//rect.Elements.Add(new PdfReal(right));
//rect.Elements.Add(new PdfReal(top));
//pdf.Internals.AddObject(rect);

//PdfDictionary form = new PdfDictionary(pdf);
//form.Elements.Add("/Filter", new PdfName("/FlateDecode"));
//form.Elements.Add("/Length", new PdfInteger(20));
//form.Elements.Add("/Subtype", new PdfName("/Form"));
//form.Elements.Add("/Type", new PdfName("/XObject"));
//pdf.Internals.AddObject(form);

//PdfDictionary appearanceStream = new PdfDictionary(pdf);
//appearanceStream.Elements.Add("/N", form);
//pdf.Internals.AddObject(appearanceStream);

//PdfDictionary textfield = new PdfDictionary(pdf);
//textfield.Elements.Add("/FT", new PdfName("/Tx"));
//textfield.Elements.Add("/Subtype", new PdfName("/Widget"));
//textfield.Elements.Add("/T", new PdfString("fldHelloWorld"));
//textfield.Elements.Add("/V", new PdfString("Hello World!"));
//textfield.Elements.Add("/Type", new PdfName("/Annot"));
//textfield.Elements.Add("/AP", appearanceStream);
//textfield.Elements.Add("/Rect", rect);
//textfield.Elements.Add("/P", page1);
//pdf.Internals.AddObject(textfield);

//PdfArray annotsArray = new PdfArray(pdf);
//annotsArray.Elements.Add(textfield);
//pdf.Internals.AddObject(annotsArray);

//page1.Elements.Add("/Annots", annotsArray);

//// draw rectangle around text field
////XGraphics gfx = XGraphics.FromPdfPage(page1);
////gfx.DrawRectangle(new XPen(XColors.DarkOrange, 2), left, 40, right, bottom - top);

//// Save document
//pdf.Save(savePath);
//pdf.Close();


//return;


System.IO.File.Copy(path, savePath, true);

using PdfDocument document = PdfReader.Open(savePath, PdfDocumentOpenMode.Modify, new PdfReaderOptions
{
    
});


// causing issues
// see this explanation https://forum.pdfsharp.net/viewtopic.php?f=2&t=3741#p12178
// document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);


// maybe this fix
https://github.com/packdat/PDFsharp/blob/AcroForms/src/PdfSharp/Pdf.AcroForms/PdfTextField.cs

var fieldBad = document.AcroForm.Fields["ResourcesBankAccountsAccountTypeDisplay1"];
var fieldGood = document.AcroForm.Fields["ResourcesBankAccountsAccountTypeDisplay4"];

Console.WriteLine("================================ Bad ================================");
Console.WriteLine($"HasKids: {fieldBad.HasKids}");
foreach (var itemBad in fieldBad.Elements)
{
    Console.WriteLine($"{itemBad.Key}: {itemBad.Value.GetType().Name}. Value is {itemBad.Value}");
}

Console.WriteLine();
Console.WriteLine("------------------- Kids ---------------------");
Console.WriteLine();
foreach (var kidBad in fieldBad.Elements.GetArray("/Kids"))
{
    Console.WriteLine($"Kid item: {kidBad.GetType().Name}. Value is {kidBad}");

    if (kidBad is PdfReference pdfReference)
    {
        
    }
}

Console.WriteLine();
Console.WriteLine("------------------- Nested Fields ---------------------");
Console.WriteLine();
foreach (var nestedFieldBad in fieldBad.Fields)
{
    Console.WriteLine($"Nested Field item: {nestedFieldBad.GetType().Name}, Value is {nestedFieldBad}");
}

Console.WriteLine($"Appearance Names: {string.Join(", ", fieldBad.GetAppearanceNames())}");

// fixup

// fieldBad.Elements.Add("/P", pdfReference);
//fieldBad.Elements.SetName(PdfAnnotation.Keys.Type, "/Annot");
//fieldBad.Elements.SetInteger(PdfAnnotation.Keys.F, (int)PdfAnnotationFlags.Print);
//fieldBad.Elements.Remove("/Kids");
// fieldBad.Elements.SetRectangle(PdfAnnotation.Keys.Rect, new PdfRectangle(new XRect(119.378, 201.864, 7, 8)));
// end fixup


Console.WriteLine();
Console.WriteLine();
Console.WriteLine();

Console.WriteLine("================================ Updated Bad ================================");
Console.WriteLine($"HasKids: {fieldBad.HasKids}");
foreach (var itemBad in fieldBad.Elements)
{
    Console.WriteLine($"{itemBad.Key}: {itemBad.Value.GetType().Name}. Value is {itemBad.Value}");
}



Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();

Console.WriteLine("================================ Good ================================");
Console.WriteLine($"HasKids: {fieldGood.HasKids}");
foreach (var itemGood in fieldGood.Elements)
{
    Console.WriteLine($"{itemGood.Key}: {itemGood.Value.GetType().Name}. Value is {itemGood.Value}");
}


var field = fieldBad;

string value = "Berel";

if (field is PdfTextField textField)
{
    SafelySetPdfTextField(textField, value);
}
else if (field is PdfCheckBoxField checkboxField && bool.TryParse(value, out var b))
{
    checkboxField.Checked = b;
}
else
{
    // for now we'll just default to setting as a string
    field.Value = new PdfString(value);
}


document.Save(savePath);

void SafelySetPdfTextField(PdfTextField textField, string value)
{
    

    // trying to be valid for this check
    // https://github.com/empira/PDFsharp/blob/master/src/foundation/src/PDFsharp/src/PdfSharp/Drawing/XForm.cs#L100
    var rect = field.Elements.GetRectangle(PdfSharp.Pdf.Annotations.PdfAnnotation.Keys.Rect);

    var canUseText = rect.Size.Width >= 1 && rect.Size.Height >= 1;

    if (canUseText)
    {
        textField.Text = value;
    }
    else
    {
        var fl = field.Fields;

        textField.Value = new PdfString(value);
    }
}
