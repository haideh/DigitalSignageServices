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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Iads" in both code and config file together.
    [ServiceContract]
    public interface Iads
    {
        [OperationContract]

        [WebInvoke(Method = "POST",
                                 ResponseFormat = WebMessageFormat.Json,
                                 RequestFormat = WebMessageFormat.Json,
                                BodyStyle = WebMessageBodyStyle.Bare,
                                UriTemplate = "digital/saveAds")]

        ResultMessage<string> saveAds(AdsInfoWTO AdsInfo);

        [OperationContract]

        [WebInvoke(Method = "POST",
                                ResponseFormat = WebMessageFormat.Json,
                                RequestFormat = WebMessageFormat.Json,
                               BodyStyle = WebMessageBodyStyle.Bare,
                               UriTemplate = "digital/deleteAds")]

        ResultMessage<string> deleteAds(long id);

        [OperationContract]

        [WebInvoke(Method = "POST",
                                ResponseFormat = WebMessageFormat.Json,
                                RequestFormat = WebMessageFormat.Json,
                               BodyStyle = WebMessageBodyStyle.Bare,
                               UriTemplate = "digital/deleteAdsItem")]

        ResultMessage<string> deleteAdsItem(long id);

        [OperationContract]

        [WebInvoke(Method = "POST",
                              ResponseFormat = WebMessageFormat.Json,
                              RequestFormat = WebMessageFormat.Json,
                             BodyStyle = WebMessageBodyStyle.Bare,
                             UriTemplate = "digital/editAds")]
        ResultMessage<string> editAds(string title, string time, long ad_id);


        #region Search
        [OperationContract]
        [WebInvoke(Method = "POST",
                               ResponseFormat = WebMessageFormat.Json,
                               RequestFormat = WebMessageFormat.Json,
                              BodyStyle = WebMessageBodyStyle.Bare,
                              UriTemplate = "digital/searchDataOnAds")]
        ResultMessage<List<AdsInfoWTO>> searchDataOnAds(AdsInfoWTO filter, PagingInfo paging);

        [OperationContract]
        [WebInvoke(Method = "POST",
                             ResponseFormat = WebMessageFormat.Json,
                             RequestFormat = WebMessageFormat.Json,
                            BodyStyle = WebMessageBodyStyle.Bare,
                            UriTemplate = "digital/searchDataOnAdsItem")]
        ResultMessage<List<AdsIemInfoWTO>> searchDataOnAdsItem(AdsIemInfoWTO filter, PagingInfo paging);

        [OperationContract]
        [WebInvoke(Method = "POST",
                             ResponseFormat = WebMessageFormat.Json,
                             RequestFormat = WebMessageFormat.Json,
                            BodyStyle = WebMessageBodyStyle.Bare,
                            UriTemplate = "digital/searchDataOnTVLives")]
        ResultMessage<List<LiveTVInfoWTO>> searchDataOnTVLives(LiveTVInfoWTO filter, PagingInfo paging);


        [OperationContract]
        [WebInvoke(Method = "POST",
                            ResponseFormat = WebMessageFormat.Json,
                            RequestFormat = WebMessageFormat.Json,
                           BodyStyle = WebMessageBodyStyle.Bare,
                           UriTemplate = "digital/getAdsWithItemDetail")]
        ResultMessage<List<AdsInfoWTO>> getAdsWithItemDetail(string type, long companyId);
        [OperationContract]
        [WebInvoke(Method = "POST",
                          ResponseFormat = WebMessageFormat.Json,
                          RequestFormat = WebMessageFormat.Json,
                         BodyStyle = WebMessageBodyStyle.Bare,
                         UriTemplate = "digital/getAdsWithItemDetail")]
        ResultMessage<List<AdsInfoWTO>> getWidgetAdsWithItemDetail(long companyId);




        #endregion

    }
}
