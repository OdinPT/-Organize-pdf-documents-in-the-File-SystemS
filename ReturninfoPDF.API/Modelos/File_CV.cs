using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReturninfoPDF.API.Modelos
{
    public class File_CV
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public  string Numero { get; set; }


        public File_CV( string nome, string email, string numero)
        {
            Nome = nome;
            Email = email;
            Numero = numero;

           // Console.WriteLine(nome + email + numero);
        }

        public override string ToString()
        {
            return String.Format("Nome:{0}, Email:{1}, Numero:{2}", Nome, Numero, Email);
        }
    }
}

