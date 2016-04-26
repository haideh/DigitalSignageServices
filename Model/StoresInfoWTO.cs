using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DigitalServices.Model
{
    [Serializable]
    [DataContract]
    public class StoresInfoWTO
    {

        [DataMember]
        public long id { get; set; }
        [DataMember]
        public int category_id { get; set; }
        [DataMember]
        public int map_id { get; set; }
        [DataMember]
        public string title { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public int unit { get; set; }
        [DataMember]
        public string phone1 { get; set; }
        [DataMember]
        public string phone2 { get; set; }
        [DataMember]
        public int status { get; set; }
        [DataMember]
        public int type { get; set; }
        [DataMember]
        public int x { get; set; }
        [DataMember]
        public int y { get; set; }
        [DataMember]
        public string image { get; set; }
        [DataMember]
        public string startWorkTime { get; set; }
        [DataMember]
        public string endWorkTime { get; set; }
        [DataMember]
        public string workingDay { get; set; }
        [DataMember]
        public long companyId { get; set; }
        [DataMember]
        public string companyName { get; set; }

    }
}