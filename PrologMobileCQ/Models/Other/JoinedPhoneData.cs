using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrologMobileCQ.Models.Other
{
    public class JoinedPhoneData
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public bool BlacklistState { get; set; }
        public string RegisteredUser { get; set; }
        public string RegisteredUserID { get; set; }
        public string RegisteredUserEmail { get; set; }
        public long RegisteredUserPhoneIMEI { get; set; }
    }
}
