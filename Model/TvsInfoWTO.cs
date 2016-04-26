using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DigitalServices.Model
{
    [Serializable]
    [DataContract]
    public class TvsInfoWTO
    {
        [DataMember]
        public long id { get; set; }
        [DataMember]
        public long category_id { get; set; }
        [DataMember]
        public long alarm_id { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string ip { get; set; }
        [DataMember]
        public int status { get; set; }
        [DataMember]
        public int content_id { get; set; }
        [DataMember]
        public int type { get; set; }
        [DataMember]
        public int isDirty { get; set; }
        [DataMember]
        public long lastAlive { get; set; }
        [DataMember]
        public int x { get; set; }
        [DataMember]
        public int y { get; set; }
        [DataMember]
        public long companyId { get; set; }
        [DataMember]
        public string companyName { get; set; }
    }
}