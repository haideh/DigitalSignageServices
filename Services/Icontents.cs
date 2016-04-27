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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Icontents" in both code and config file together.
    [ServiceContract]
    public interface Icontents
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
                                 ResponseFormat = WebMessageFormat.Json,
                                 RequestFormat = WebMessageFormat.Json,
                                BodyStyle = WebMessageBodyStyle.Bare,
                                UriTemplate = "digital/getNewContentId")]
        ResultMessage<int> getNewContentId();

        [OperationContract]
        [WebInvoke(Method = "POST",
                                 ResponseFormat = WebMessageFormat.Json,
                                 RequestFormat = WebMessageFormat.Json,
                                BodyStyle = WebMessageBodyStyle.Bare,
                                UriTemplate = "digital/addContent")]
        ResultMessage<string> addContent(ContentInfoWTO contentInfo);

        [OperationContract]
        [WebInvoke(Method = "POST",
                                 ResponseFormat = WebMessageFormat.Json,
                                 RequestFormat = WebMessageFormat.Json,
                                BodyStyle = WebMessageBodyStyle.Bare,
                                UriTemplate = "digital/editContentOption")]
        ResultMessage<string> editContentOption(ContentOptionInfoWTO contentOptionInfo);

        [OperationContract]
        [WebInvoke(Method = "POST",
                                ResponseFormat = WebMessageFormat.Json,
                                RequestFormat = WebMessageFormat.Json,
                               BodyStyle = WebMessageBodyStyle.Bare,
                               UriTemplate = "digital/editContentOptionLive")]
        ResultMessage<string> editContentOptionLive(ContentOptionInfoWTO contentOptionInfo);

        [OperationContract]
        [WebInvoke(Method = "POST",
                                 ResponseFormat = WebMessageFormat.Json,
                                 RequestFormat = WebMessageFormat.Json,
                                BodyStyle = WebMessageBodyStyle.Bare,
                                UriTemplate = "digital/deleteContents")]
        ResultMessage<string> deleteContents(long id);

      

        [OperationContract]
        [WebInvoke(Method = "POST",
                                ResponseFormat = WebMessageFormat.Json,
                                RequestFormat = WebMessageFormat.Json,
                               BodyStyle = WebMessageBodyStyle.Bare,
                               UriTemplate = "digital/saveContentAds")]
         ResultMessage<string> saveContentAds(ContentOptionInfoWTO contentOptionInfo);

        #region Search

        [OperationContract]
        [WebInvoke(Method = "POST",
                               ResponseFormat = WebMessageFormat.Json,
                               RequestFormat = WebMessageFormat.Json,
                              BodyStyle = WebMessageBodyStyle.Bare,
                              UriTemplate = "digital/searchContentsWithAdsItemDetail")]
        ResultMessage<List<AdsInfoWTO>> searchContentsWithAdsItemDetail(string type,  long content_id,int position);

        [OperationContract]
        [WebInvoke(Method = "POST",
                              ResponseFormat = WebMessageFormat.Json,
                              RequestFormat = WebMessageFormat.Json,
                             BodyStyle = WebMessageBodyStyle.Bare,
                             UriTemplate = "digital/loadContentsWithAdsItemDetail")]
        ResultMessage<List<AdsInfoWTO>> loadContentsWithAdsItemDetail( long content_id);

        [OperationContract]
        [WebInvoke(Method = "POST",
                           ResponseFormat = WebMessageFormat.Json,
                           RequestFormat = WebMessageFormat.Json,
                          BodyStyle = WebMessageBodyStyle.Bare,
                          UriTemplate = "digital/loadLiveContentsAds")]
        ResultMessage<List<LiveTVInfoWTO>> loadLiveContentsAds( long content_id);

        [OperationContract]
        [WebInvoke(Method = "POST",
                         ResponseFormat = WebMessageFormat.Json,
                         RequestFormat = WebMessageFormat.Json,
                        BodyStyle = WebMessageBodyStyle.Bare,
                        UriTemplate = "digital/getContentList")]
        ResultMessage<List<ContentInfoWTO>> getContentList(ContentInfoWTO filter, PagingInfo paging);

        [OperationContract]
        [WebInvoke(Method = "POST",
                       ResponseFormat = WebMessageFormat.Json,
                       RequestFormat = WebMessageFormat.Json,
                      BodyStyle = WebMessageBodyStyle.Bare,
                      UriTemplate = "digital/searchDataContent")]
        ResultMessage<List<ContentInfoWTO>> searchDataContent(ContentInfoWTO filter, PagingInfo paging);

        [OperationContract]
        [WebInvoke(Method = "POST",
                       ResponseFormat = WebMessageFormat.Json,
                       RequestFormat = WebMessageFormat.Json,
                      BodyStyle = WebMessageBodyStyle.Bare,
                      UriTemplate = "digital/searchDataContentType")]
        ResultMessage<List<ContentTypeWTO>> searchDataContentType(ContentTypeWTO filter, PagingInfo paging);

        [OperationContract]
        [WebInvoke(Method = "POST",
                    ResponseFormat = WebMessageFormat.Json,
                    RequestFormat = WebMessageFormat.Json,
                   BodyStyle = WebMessageBodyStyle.Bare,
                   UriTemplate = "digital/searchContentId")]
        ResultMessage<ContentInfoWTO> searchContentId(string ip, string companyName);

        #endregion
    }
}
