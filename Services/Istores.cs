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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Istores" in both code and config file together.
    [ServiceContract]
    public interface Istores
    {
        [OperationContract]

        [WebInvoke(Method = "POST",
                                ResponseFormat = WebMessageFormat.Json,
                                RequestFormat = WebMessageFormat.Json,
                               BodyStyle = WebMessageBodyStyle.Bare,
                               UriTemplate = "digital/addStore")]

        ResultMessage<string> addStore(StoresInfoWTO stores);

        [OperationContract]

        [WebInvoke(Method = "POST",
                                ResponseFormat = WebMessageFormat.Json,
                                RequestFormat = WebMessageFormat.Json,
                               BodyStyle = WebMessageBodyStyle.Bare,
                               UriTemplate = "digital/editStore")]

        ResultMessage<string> editStore(StoresInfoWTO stores);

        [OperationContract]

        [WebInvoke(Method = "POST",
                                ResponseFormat = WebMessageFormat.Json,
                                RequestFormat = WebMessageFormat.Json,
                               BodyStyle = WebMessageBodyStyle.Bare,
                               UriTemplate = "digital/deleteStore")]

        ResultMessage<string> deleteStore(long id);
        [OperationContract]
        [WebInvoke(Method = "GET",
                             ResponseFormat = WebMessageFormat.Json,
                             RequestFormat = WebMessageFormat.Json,
                            BodyStyle = WebMessageBodyStyle.Bare,
                            UriTemplate = "digital/searchDataOnStores")]

        #region Search
        ResultMessage<List<StoresInfoWTO>> searchDataOnStores(StoresInfoWTO storesInfoWTO , PagingInfo paging);       
        #endregion



    }
}
