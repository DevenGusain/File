using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace FMS.Models
{
    public class droplist
    {
        public droplist() 
        {
            this.loc_list = new List<location_list>();
            this.sec_list = new List<section_list>();
            this.alm_list = new List<almirah_list>(); 
            this.par_list = new List<part_list>();            
            this.rows_list= new List<row_list>();
            this.cols_list= new List<col_list>();
        }

        public List<location_list> loc_list { get; set; }
        public List<section_list> sec_list { get; set; }
        public List<almirah_list> alm_list { get; set; }
        public List<part_list> par_list { get; set; }
        public List<row_list> rows_list { get; set; }
        public List<col_list> cols_list { get; set; }
    }
    public class location_list{         
        public string Location { get; set; }       
    }

    public class section_list {public string Section { get; set; } }

    public class almirah_list {public string Almirah_No { get; set; }}

    public class part_list {public string Part_No { get; set; }}

    public class row_list { public string Row_No { get; set; } }

    public class col_list { public string Col_No { get; set; } }
}
