using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace FMS.Models
{

    public class dropdown
    {        
        public List<masterTable> mtlist { get; set; }

    }


    public class masterTable 
    {
        public string File_id { get; set; }

        public string File_name { get; set; }

        public string File_num { get; set; }

        public string section{ get; set; }

        public string location { get; set; }

        public string almirah { get; set; }


        public string part { get; set; }

        public string row { get; set; }

        public string col { get; set; }
    }
}
