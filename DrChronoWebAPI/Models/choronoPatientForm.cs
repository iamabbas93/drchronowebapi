using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace DrChronoWebAPI.Models
{
    public class choronoPatientForm
    { 
        public int? DId { get; set; }
       
        public string First_Name { get; set; }
      
        public string Last_Name { get; set; }
    
        public string DOB { get; set; }

        public string Gender { get; set; }

        public string scheduled_time { get; set; }

        public IFormFile Patient_Image_File { get; set; }
       
      
        
        public IFormFile Insurance_Front_Image_File { get; set; }
        
     

        public IFormFile Insurance_Back_Image_File { get; set; }
         

    }
}
