using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DigitalServices.Model
{
    [Serializable]
    [DataContract]
    public class LiveTVInfoWTO
    {
        [DataMember]
        public long id { get; set; }
        [DataMember]
        public int nameId { get; set; }
        [DataMember]
        public int status { get; set; }
        [DataMember]
        public int type { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string channel { get; set; }

        [DataMember]
        public int interval { get; set; }
        [DataMember]
        public long content_ad_id { get; set; }
        [DataMember]
        public int position { get; set; }
        [DataMember]
        public long companyId { get; set; }
        [DataMember]
        public string companyName { get; set; }

    }
}