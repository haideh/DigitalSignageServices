using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DigitalServices.Model
{
    [Serializable]
    [DataContract]
    public class TvContentsInfoWTO
    {
        //id, tv_id, content_id, startTime, endTime, duration, status, type, lastViewed, companyId
        [DataMember]
        public long id { get; set; }
        [DataMember]
        public long tv_id { get; set; }
        [DataMember]
        public long content_id { get; set; }
        [DataMember]
        public long startTime { get; set; }
        [DataMember]
        public long endTime { get; set; }
        [DataMember]
        public int duration { get; set; }
        [DataMember]
        public short status { get; set; }
        [DataMember]
        public long lastViewed { get; set; }
        [DataMember]
        public long companyId { get; set; }
        [DataMember]
        public long lastAlive { get; set; }

        [DataMember]
        public string ip { get; set; }
        [DataMember]
        public int type { get; set; }
        [DataMember]
        public int isDirty { get; set; }

        [DataMember]
        public int x { get; set; }
        [DataMember]
        public int y { get; set; }

    }
}