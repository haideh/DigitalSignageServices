using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DigitalServices.Model
{
    [Serializable]
    [DataContract]
    public class UserInfoWTO
    {
        [DataMember]
        public long id { get; set; }
        [DataMember]
        public string fName { get; set; }
        [DataMember]
        public string lName { get; set; }
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public int companyId { get; set; }
        
    }
}