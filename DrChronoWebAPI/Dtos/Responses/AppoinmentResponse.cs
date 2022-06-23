using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrChronoWebAPI.Dtos.Responses
{
    public class AppoinmentResponse
    {
        public object data { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
