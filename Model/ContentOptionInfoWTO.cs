using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DigitalServices.Model
{
    public class ContentOptionInfoWTO
    {

        [DataMember]
        public long id { get; set; }
        [DataMember]
        public long content_id { get; set; }
        [DataMember]
        public int position { get; set; }
        [DataMember]
        public int second { get; set; }
        [DataMember]
        public int shuffle { get; set; }
        [DataMember]
        public int interval { get; set; }
        [DataMember]
        public long live_id { get; set; }
        [DataMember]
        public long ad_id { get; set; }
        [DataMember]
        public long createDateTime { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public int secondViewed { get; set; }
        [DataMember]
        public long companyId { get; set; }
        [DataMember]
        public string companyName { get; set; }


    }
}