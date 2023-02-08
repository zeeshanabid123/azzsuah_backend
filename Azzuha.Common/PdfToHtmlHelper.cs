using EvoPdf;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http.Headers;


namespace Azzuha.Common
{
    public class PdfToHtmlHelper
    {
        private HtmlToPdfConverter htmlToPdfConverter = null;

        public PdfToHtmlHelper(string licenseKey)
        {
            htmlToPdfConverter = new HtmlToPdfConverter();
            htmlToPdfConverter.LicenseKey = licenseKey;
            htmlToPdfConverter.HtmlViewerWidth = 1024;
            htmlToPdfConverter.PdfDocumentOptions.PdfPageSize = SelectedPdfPageSize("A4");
            htmlToPdfConverter.PdfDocumentOptions.PdfPageOrientation = SelectedPdfPageOrientation("Portrait");
            htmlToPdfConverter.NavigationTimeout = 60;
            htmlToPdfConverter.ConversionDelay = 5;
        }


        public Tuple<IFormFile, byte[]> GetPdfFileFromHtml(string htmlString)
        {
            try
            {
                return MakeFileFromByteArray(htmlToPdfConverter.ConvertHtml(htmlString, ""));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private Tuple<IFormFile, byte[]> MakeFileFromByteArray(byte[] bufferArray)
        {
            try
            {
                IFormFile pdfFile = null;

                MemoryStream mem = new MemoryStream(bufferArray);
                pdfFile = new FormFile(mem, 0, mem.Length, "MonthlyReport", "MonthlyReport")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/pdf"
                };

                return Tuple.Create(pdfFile, bufferArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static PdfPageSize SelectedPdfPageSize(string selectedValue)
        {
            switch (selectedValue)
            {
                case "A0":
                    return PdfPageSize.A0;
                case "A1":
                    return PdfPageSize.A1;
                case "A10":
                    return PdfPageSize.A10;
                case "A2":
                    return PdfPageSize.A2;
                case "A3":
                    return PdfPageSize.A3;
                case "A4":
                    return PdfPageSize.A4;
                case "A5":
                    return PdfPageSize.A5;
                case "A6":
                    return PdfPageSize.A6;
                case "A7":
                    return PdfPageSize.A7;
                case "A8":
                    return PdfPageSize.A8;
                case "A9":
                    return PdfPageSize.A9;
                case "ArchA":
                    return PdfPageSize.ArchA;
                case "ArchB":
                    return PdfPageSize.ArchB;
                case "ArchC":
                    return PdfPageSize.ArchC;
                case "ArchD":
                    return PdfPageSize.ArchD;
                case "ArchE":
                    return PdfPageSize.ArchE;
                case "B0":
                    return PdfPageSize.B0;
                case "B1":
                    return PdfPageSize.B1;
                case "B2":
                    return PdfPageSize.B2;
                case "B3":
                    return PdfPageSize.B3;
                case "B4":
                    return PdfPageSize.B4;
                case "B5":
                    return PdfPageSize.B5;
                case "Flsa":
                    return PdfPageSize.Flsa;
                case "HalfLetter":
                    return PdfPageSize.HalfLetter;
                case "Ledger":
                    return PdfPageSize.Ledger;
                case "Legal":
                    return PdfPageSize.Legal;
                case "Letter":
                    return PdfPageSize.Letter;
                case "Letter11x17":
                    return PdfPageSize.Letter11x17;
                case "Note":
                    return PdfPageSize.Note;
                default:
                    return PdfPageSize.A4;
            }
        }

        private static PdfPageOrientation SelectedPdfPageOrientation(string selectedValue)
        {
            return (selectedValue == "Portrait") ? PdfPageOrientation.Portrait : PdfPageOrientation.Landscape;
        }
    }
}
