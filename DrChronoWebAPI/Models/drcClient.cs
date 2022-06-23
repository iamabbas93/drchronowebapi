using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrChronoWebAPI.Models
{
    public class drcClient
    {
        [Key]
        public int id { get; set; }

        public string clientId { get; set; }

        public string clientSecret { get; set; }

        public string authorizationEndpoint { get; set; }

        public string tokenEndpoint { get; set; }

        public string redirectUri { get; set; }

        public int? doctorId { get; set; }

        public string userName { get; set; }
        public string password { get; set; }


        public string? createdBy { get; set; }

        public DateTime? createdDate { get; set; }
        public string? updateBy { get; set; }
        public DateTime? updateDate { get; set; }


    }
}
