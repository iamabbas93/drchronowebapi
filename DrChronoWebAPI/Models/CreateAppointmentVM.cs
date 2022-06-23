using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrChronoWebAPI.Models
{
  
    public class CreateAppointmentVM
    {
        public int doctor { get; set; }
        public int patient { get; set; }
        public int exam_room { get; set; }
        public int duration { get; set; }
        public string scheduled_time { get; set; }
        public int office { get; set; }
        public List<string> icd10_codes { get; set; }
        public List<CustomField> custom_fields { get; set; }
    }
    public class CustomField
    {
        public int field_type { get; set; }
        public string field_value { get; set; }
    }

   

}
