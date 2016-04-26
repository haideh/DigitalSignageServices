﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DigitalServices.Model
{
    public class ContentInfoWTO
    {
        //id, tv_id, content_id, startTime, endTime, duration, status, type, lastViewed

        [DataMember]
        public long id { get; set; }
        [DataMember]
        public long tv_id { get; set; }
        [DataMember]
        public long content_id { get; set; }
        [DataMember]
        public int startTime { get; set; }
        [DataMember]
        public int endTime { get; set; }
        [DataMember]
        public int duration { get; set; }
        [DataMember]
        public int status { get; set; }
        [DataMember]
        public int type { get; set; }
        [DataMember]
        public long lastViewed { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string description { get; set; }

        [DataMember]
        public string file_name { get; set; }

        [DataMember]
        public string ad_title { get; set; }
        [DataMember]
        public long companyId { get; set; }
        [DataMember]
        public string companyName { get; set; }


    }
}