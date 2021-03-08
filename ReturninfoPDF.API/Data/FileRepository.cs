using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using ReturninfoPDF.API.Dtos;
using ReturninfoPDF.API.Modelos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Path = System.IO.Path;

namespace ReturninfoPDF.API.Data
{
    public class FileRepository : IFileRepository
    {
        public FileRepository()
        {

        }

        public StringBuilder ConcatDataPDF(string dirs)
        {
            StringBuilder sb = new StringBuilder();

            PdfReader reader = new PdfReader(dirs);
            Rectangle rect = new Rectangle(0, 0, 415, 775);

            RenderFilter[] filter = { new RegionTextRenderFilter(rect) };
            ITextExtractionStrategy strategy;

            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                strategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), filter);
                sb.AppendLine(PdfTextExtractor.GetTextFromPage(reader, i, strategy));
            }
            return sb;
        }

        public StringBuilder ConcatAllDataPDF(string dirs)
        {
            StringBuilder sb = new StringBuilder();
            PdfReader reader = new PdfReader(dirs);
            Rectangle rect = new Rectangle(0, 0,775,775);
            RenderFilter[] filter = { new RegionTextRenderFilter(rect) };
            ITextExtractionStrategy strategy;

            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                strategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), filter);
                sb.AppendLine(PdfTextExtractor.GetTextFromPage(reader, i, strategy));
            }
            return sb;
        }

        public void makefile(StringBuilder sb, string name)
        {
            using StreamWriter sw = System.IO.File.CreateText(name);

            sw.WriteLine(sb.ToString());
            sw.Close();
        }
        //alias de retornar uma lista chama logo a outra função
        public string[] makelist( string tempfile)
        {
           
            var list = new List<string>();

            var fileStream = new FileStream(@tempfile, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
            return list.ToArray();
        }
      
        public string GetMouthString()
        {
            DateTimeFormatInfo mfi = new DateTimeFormatInfo();
            string strMonthName = mfi.GetMonthName(DateTime.Now.Month).ToString();

            return strMonthName;
        }

        public void SaveAndMoveFile ( string EntidadePDF, string dir)
        {
            EntidadePDF = char.ToUpper(EntidadePDF[0]) + EntidadePDF.Substring(1);
            var strmonthName = GetMouthString();
            string path = @"C:\Project_Docs";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Directory.CreateDirectory(path + @"\" + DateTime.Now.Year.ToString() + @"\" + EntidadePDF + @"\" + strmonthName);
                string destinationFile01 = @path + @"\" + DateTime.Now.Year.ToString() + @"\" + EntidadePDF + @"\" + strmonthName + @"\" + EntidadePDF + DateTime.Now.ToString("yyMMddHHmmsss") + @".pdf";

                System.IO.File.Copy(dir, destinationFile01);
            }
            else
            {             
                //save files in c:\currentYear\EntityDoc\CurrentMonth\doc.pdf

                Directory.CreateDirectory(path + @"\" + DateTime.Now.Year.ToString() + @"\" + EntidadePDF + @"\" + strmonthName);
                string destinationFile01 = @path + @"\" + DateTime.Now.Year.ToString() + @"\" + EntidadePDF + @"\" + strmonthName + @"\" + EntidadePDF + DateTime.Now.ToString("yyMMddHHmmsss") + @".pdf";

                System.IO.File.Copy(dir, destinationFile01);
            }
        }

        public string ChanceSlash(string directory)
        {
            using FileStream fs = System.IO.File.Open(directory, FileMode.Open, FileAccess.Read, FileShare.Read);
            var t = fs.Name.Replace("\"", "/");

            return t;
            //throw new NotImplementedException();
        }
    }
}

