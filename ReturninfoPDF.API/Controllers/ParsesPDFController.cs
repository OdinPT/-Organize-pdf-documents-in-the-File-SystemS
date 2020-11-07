using AutoMapper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using PdfSharp.Pdf;
using ReturninfoPDF.API.Data;
using ReturninfoPDF.API.Dtos;
using ReturninfoPDF.API.Modelos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using Path = System.IO.Path;
using PdfDocument = PdfSharp.Pdf.PdfDocument;

namespace ReturninfoPDF.API.Controllers
{
    [Route("api/parsepdf/")]
    [ApiController]
    public class ParsesPDFController : ControllerBase
    {
        private readonly IFileRepository _repo;
        private readonly IMapper _mapper;

        // temp file
        public const string FILETEMP = "temp.txt";

        public ParsesPDFController(IFileRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public void CV_check(dirDto dirdto)
        {
            

            var sb = _repo.ConcatAllDataPDF(dirdto.dir);
            _repo.makefile(sb, FILETEMP);

            var list = _repo.makelist(FILETEMP);

            var myString = ".pt";
            var newList = list.Where(x => x.Contains(myString)).ToList();

             
            if (newList.Count() == 0)
            {
                _repo.SaveAndMoveFile("Unknown",  dirdto.dir);
                Console.WriteLine("Unknown");
            } else {

                var EntidadePDF = newList[0];
                 //verifica se existe www.XXX.com no pdf se não existir ele retorna linha então depois é retirar o link de lá.
                var existeURL = EntidadePDF.Contains("www.");

           
               if (existeURL == true) {

                 var charsToRemove = new string[] { "www.", ".pt" };
                 foreach (var c in charsToRemove) {
                    EntidadePDF = EntidadePDF.Replace(c, string.Empty);
                    EntidadePDF = EntidadePDF.Split('*').Last();

                    Console.WriteLine(EntidadePDF);
                 }

               } else {
              

                 string[] s = EntidadePDF.Split(".pt");
                 EntidadePDF = s[0];

                 //  procurar por last word from array
                 EntidadePDF = EntidadePDF.Split(' ').Last();

                 Console.WriteLine(EntidadePDF);
                    Console.WriteLine("teste");
               }

            _repo.SaveAndMoveFile(EntidadePDF, dirdto.dir);
                //pesquisar por ocr para analisar pelo nif no documento e depois pesquisar na net o niff e assim saber a entidade e quardar em documento


            }
        }  
    }
}