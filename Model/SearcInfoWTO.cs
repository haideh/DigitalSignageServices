using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DigitalServices.Model
{
    [Serializable]
    [DataContract]
    public class FieldInfo
    {
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string type { get; set; }
    }
    [Serializable]
    [DataContract]
    public class OrderInfo
    {
        [DataMember]
        public string orderName { get; set; } = "id";
        [DataMember]
        public string sortType { get; set; } = "desc";
    }
    [Serializable]
    [DataContract]
    public class PagingInfo
    {
        [DataMember]
        public List<OrderInfo> orderLst { get; set; }
        [DataMember]
        public int pageNumber { get; set; } = 1;
        [DataMember]
        public int pageSize { get; set; } = 100;

    }
}