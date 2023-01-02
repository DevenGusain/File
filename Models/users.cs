

using Microsoft.EntityFrameworkCore;

namespace FMS.Models
{
    public class users  
    {
        public string id{ get; set; }

        public string username{ get; set; }

        public string password { get; set; }     
    }


    public class usersDBcontext { 
        public class usersDBContext { }

        
    }

    
}


