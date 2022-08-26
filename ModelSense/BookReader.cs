using System;
using Windows.Data.Pdf;
using Windows.UI.Xaml;

namespace ModelSense
{
    public sealed partial class BookReader
    {
        static PdfDocument pdf { get; set; }
        public void ReadEBook(string v, string psword)
        {
            bool passwordprotection = pdf.IsPasswordProtected;
            if (passwordprotection == false)
            {
                ReadPdf(PdfFile.Read(v));
            }
            else
            {
                ReadPdf(PdfFile.Read(v), psword);
            }
        }

        public extern void ReadPdf(PdfFile file);
        public extern void ReadPdf(PdfFile file, string password);
    }
    public sealed class PdfFile
    {
        public static PdfFile Read(string v)
        {
            return (PdfFile)v;
        }

        public static explicit operator PdfFile(string v)
        {
            return (PdfFile)v;
        }
    }
}
