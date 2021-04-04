using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReturninfoPDF.API.Modelos
{
    public class Files
    {
        public int Id_Files { get; set; }
        
        public String Name_Files { get; set; }
         
        // veio de .pdf ou scan?
        public String Ocr_Files { get; set; }
        
        public DateTime Date_Upload_Files { get; set; }
        
        public String Dir_Save_Files { get; set; }

        public string All_Data_Files { get; set; }

        //Fatura ou nota de crédito ou débito
        public string Type_doc_Files { get; set; }
        
        //remetente do documento
        public string Sender_doc_Files { get; set; }
        //Destinatário
        public string Receiver_doc_Files { get; set; }

    }
}
