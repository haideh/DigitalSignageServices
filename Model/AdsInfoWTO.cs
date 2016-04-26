using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DigitalServices.Model
{
    [Serializable]
    [DataContract]
    public class AdsInfoWTO
    {

        [DataMember]
        public long id { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string adItemTitle { get; set; }
        [DataMember]
        public string adItemDesc { get; set; }
        [DataMember]
        public string adItemFeedTitle { get; set; }
        [DataMember]
        public string adItemImageFileName { get; set; }
        [DataMember]
        public string adItemVedioFileName { get; set; }
        [DataMember]
        public string adItemFeedUrl { get; set; }
        [DataMember]
        public int status { get; set; }
        [DataMember]
        public int type { get; set; }
        [DataMember]
        public int max_minutes { get; set; }
        [DataMember]
        public int passed_minutes { get; set; }

        [DataMember]
        public List<AdsIemInfoWTO> itemList { get; set; }

        [DataMember]
        public int shuffle { get; set; }
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