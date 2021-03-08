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
        
        public String Local_Save_Files { get; set; }

        public string All_Data_Files { get; set; }

    }
}
