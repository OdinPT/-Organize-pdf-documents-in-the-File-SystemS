
using ReturninfoPDF.API.Dtos;
using ReturninfoPDF.API.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturninfoPDF.API.Data
{
    public interface IFileRepository
    {
        StringBuilder ConcatDataPDF(string dirs);

        StringBuilder ConcatAllDataPDF(string dirs);

        void makefile(StringBuilder sb, string name);
        string[] makelist(string temp);

        string GetMouthString();

        void SaveAndMoveFile(string EntidadePDF, string dir);

    }
}

