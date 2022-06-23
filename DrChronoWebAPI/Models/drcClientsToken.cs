using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrChronoWebAPI.Models
{
    public class drcClientsToken
    {
        [Key]
        public int tokenId { get; set; }

        public int? doctorId { get; set; }

        public string access_token { get; set; }

        public string refresh_token { get; set; }

        public long? expires_in { get; set; }

        public string token_type { get; set; }

        public string scope { get; set; }

        public string code { get; set; }


        public string state { get; set; }

        public bool? isActive { get; set; }



        public DateTime? createdDate { get; set; }

        public DateTime? expiryDate { get; set; }


        public string? createdBy { get; set; }

        public string? updateBy { get; set; }

        public DateTime? updateDate { get; set; }
    }
}
