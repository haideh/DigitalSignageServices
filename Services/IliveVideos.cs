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
    [ServiceContract]
    public interface IliveVideos
    {
        [OperationContract]

        [WebInvoke(Method = "POST",
                                 ResponseFormat = WebMessageFormat.Json,
                                 RequestFormat = WebMessageFormat.Json,
                                BodyStyle = WebMessageBodyStyle.Bare,
                                UriTemplate = "digital/addLiveVideo")]

        ResultMessage<string> addLiveVideo(LiveTVInfoWTO videos);

        [OperationContract]
        [WebInvoke(Method = "POST",
                   ResponseFormat = WebMessageFormat.Json,
                   RequestFormat = WebMessageFormat.Json,
                  BodyStyle = WebMessageBodyStyle.Bare,
                  UriTemplate = "digital/loadLiveContentsAds")]
        ResultMessage<List<LiveTVInfoWTO>> loadLiveContentsAds(long content_id);

        [OperationContract]
        [WebInvoke(Method = "POST",
                   ResponseFormat = WebMessageFormat.Json,
                   RequestFormat = WebMessageFormat.Json,
                  BodyStyle = WebMessageBodyStyle.Bare,
                  UriTemplate = "digital/loadLiveContentsAds")]
        ResultMessage<List<LiveTVInfoWTO>> loadLivesTvAds(long content_id, long companyId, int position);

        [OperationContract]
        [WebInvoke(Method = "POST",
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare,
          UriTemplate = "digital/loadLiveContentsAds")]
        ResultMessage<List<LiveTVInfoWTO>> searchContentsWithLiveItem(long content_id, int position);
    }
}