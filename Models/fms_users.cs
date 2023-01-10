

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FMS.Models
{
    public class fms_users  
    {
        public string Id{ get; set; }

        [Required]
        public string Username{ get; set; }


        [Required]        
        public string Password { get; set; }     
    }


   
    
}


