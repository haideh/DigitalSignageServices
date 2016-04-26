using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DigitalServices.Model
{
    [Serializable]
    [DataContract]
    public class AlarmsInfoWTO
    {
        
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public int type { get; set; }
        [DataMember]
        public int status { get; set; }
        [DataMember]
        public long companyId { get; set; }
        [DataMember]
        public string companyName { get; set; }
    }
}