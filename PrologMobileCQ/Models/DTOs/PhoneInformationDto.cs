using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrologMobileCQ.Models.DTOs
{
    public class PhoneInformationDto
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public DateTime CreatedAt { get; set; }
        public long IMEI { get; set; }
        public bool Blacklist { get; set; }
    }
}
