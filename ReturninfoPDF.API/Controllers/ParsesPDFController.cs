using AutoMapper;
using IronOcr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using ReturninfoPDF.API.Data;
using ReturninfoPDF.API.Dtos;
using System;
using System.Linq;

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

        //testar, enviar ficheiro ao inve
        //public void CV_check(dirDto dirdto)
        public void CV_check(dirDto dirdto)
        {
            Console.WriteLine(dirdto.dir);
            var sb = _repo.ConcatAllDataPDF(dirdto.dir);
            _repo.makefile(sb, FILETEMP);

            var list = _repo.makelist(FILETEMP);

            var myString = ".pt";
            var newList = list.Where(x => x.Contains(myString)).ToList();

              //entra aqui quando o documento é composto por uma imagem dentro do pdf

            if (newList.Count() == 0)
            {
                _repo.SaveAndMoveFile("Unknown",  dirdto.dir);
                Console.WriteLine("Unknown");
                Console.WriteLine("entrou no if ");

                var Ocr = new IronTesseract(); // nothing to configure
                Console.WriteLine(dirdto.dir);
                //using (var Input = new OcrInput(@"C:\Users\leona\Documents\documentação_P\cc_pic.pdf")) 
                using (var Inputdata = new OcrInput(dirdto.dir)) 
                {
                    
                    Console.WriteLine(Ocr.Read(dirdto.dir));

                    var Results = Ocr.Read(dirdto.dir);
                    Console.WriteLine(Results.Text);

                }

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