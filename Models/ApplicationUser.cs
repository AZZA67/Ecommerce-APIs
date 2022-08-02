using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string NationalID { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
   
     
    }
}
