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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using Path = System.IO.Path;
using PdfDocument = PdfSharp.Pdf.PdfDocument;

namespace ReturninfoPDF.API.Controllers
{
    [Route("api/file/")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileRepository _repo;
        private readonly IMapper _mapper;

        public FileController(IFileRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        [HttpGet] // trocar pelo get
        public IActionResult CV_check(dirDto dirdto)
        {
            var sb = _repo.ConcatDataPDF(dirdto.dir);

            string[] lines = sb.ToString().Split(Environment.NewLine.ToCharArray());

            string word = lines[0];

            int x = word.IndexOf(" ") + 1;
            string str = word.Substring(x);
            int z = str.IndexOf(" ") + 1;
            string nome = str.Substring(z);

            File_CV emp3 = new File_CV(nome, lines[2], lines[4]);
            Console.WriteLine(emp3.ToString());

            return Ok(emp3);

        }
    }
}
//passar uma string com o local + nome do ficheiro
//Coloca no controller pra receber a String do caminho do PDF