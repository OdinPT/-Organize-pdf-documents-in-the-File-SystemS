using AutoMapper;
using IronOcr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using ReturninfoPDF.API.Data;
using ReturninfoPDF.API.Dtos;
using System;
using System.IO;
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
        public void CV_check(dirDto dirdto)
        {
            Console.WriteLine(dirdto.dir);
            var sb = _repo.ConcatAllDataPDF(dirdto.dir);
            _repo.makefile(sb, FILETEMP);

            var list = _repo.makelist(FILETEMP);

            foreach (object o in list)
            {
                Console.WriteLine(o);
            }

            var myString = ".pt";
            var newList = list.Where(x => x.Contains(myString)).ToList();

            if (newList.Count() == 0)
            {
                //fiquei aqui
                //quando a fatura é originada a partir de scan (foto) entra aqui a ideia é varrer a fatura e descobrir o url da empresa se nao ouver colocar como desconhecido
                //depois colocar ela a criar base de dados e a armazenar nome do ficheiro, localizaçáo entidade e também de existe SN no caso de fatura de algum equipamento.

                Console.WriteLine("Unknown", dirdto.dir);

                var Ocr = new IronTesseract();
                Console.WriteLine(dirdto.dir);

                //cange "/" to "\"
                using FileStream fs = System.IO.File.Open(dirdto.dir, FileMode.Open, FileAccess.Read, FileShare.Read);
                Console.WriteLine(dirdto.dir);
                var t = fs.Name.Replace("\"", "/");


                Console.WriteLine("depois ");
                Console.WriteLine(t);

                var sbx = _repo.ConcatAllDataPDF(dirdto.dir);
                _repo.makefile(sbx, FILETEMP);

                var listx = _repo.makelist(FILETEMP);

                foreach (object o in listx)
                {
                    Console.WriteLine(o);
                }

                Console.WriteLine("<br>");

                var myStringx = ".pt";
                var newListx = list.Where(Y => Y.Contains(myStringx)).ToList();

                Console.WriteLine("<br>");

                foreach (object o in newListx)
                {
                    Console.WriteLine(" =>" + o);
                }

                //_repo.SaveAndMoveFile("Unknown", t);
                //Console.WriteLine("Unknown", t);

               
                var lastWord = t.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).Last();
                                                                                                                       //retira testo antes do .pdf
                var a = lastWord.Split('.');
                Console.WriteLine(a[0]);

                _repo.SaveAndMoveFile(a[0], t);

            }
            else {

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