﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DigitalServices.Model
{
    public class MapsInfoWTO
    {
        [DataMember]
        public long id { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public int floor { get; set; }
        [DataMember]
        public string image { get; set; }
        [DataMember]
        public int status { get; set; }
        [DataMember]
        public int type { get; set; }
        [DataMember]
        public long companyId { get; set; }
        [DataMember]
        public string companyName { get; set; }
    }
}