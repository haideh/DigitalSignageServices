using Aryaban.Engine.Core.WebService;
using DigitalServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DigitalServices.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Imaps" in both code and config file together.
    [ServiceContract]
    public interface Imaps
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
                             ResponseFormat = WebMessageFormat.Json,
                             RequestFormat = WebMessageFormat.Json,
                            BodyStyle = WebMessageBodyStyle.Bare,
                            UriTemplate = "digital/searchDataOnMaps")]

        ResultMessage<List<MapsInfoWTO>> searchDataOnMaps(MapsInfoWTO mapsInfoWTO, PagingInfo paging);
    }
}
