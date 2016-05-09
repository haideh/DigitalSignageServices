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
    public interface Itvs
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
                                 ResponseFormat = WebMessageFormat.Json,
                                 RequestFormat = WebMessageFormat.Json,
                                BodyStyle = WebMessageBodyStyle.Bare,
                                UriTemplate = "digital/addTV")]
        ResultMessage<string> addTV(TvsInfoWTO tvsInfo);

        [OperationContract]
        [WebInvoke(Method = "POST",
                                 ResponseFormat = WebMessageFormat.Json,
                                 RequestFormat = WebMessageFormat.Json,
                                BodyStyle = WebMessageBodyStyle.Bare,
                                UriTemplate = "digital/editTV")]
        ResultMessage<string> editTV(TvsInfoWTO tvsInfo);

        [OperationContract]
        [WebInvoke(Method = "POST",
                                 ResponseFormat = WebMessageFormat.Json,
                                 RequestFormat = WebMessageFormat.Json,
                                BodyStyle = WebMessageBodyStyle.Bare,
                                UriTemplate = "digital/addCategory")]
        ResultMessage<string> addCategory(TvCategoreisInfoWTO tvCategoreisInfo);

        [OperationContract]
        [WebInvoke(Method = "POST",
                                ResponseFormat = WebMessageFormat.Json,
                                RequestFormat = WebMessageFormat.Json,
                               BodyStyle = WebMessageBodyStyle.Bare,
                               UriTemplate = "digital/deleteTVS")]
        ResultMessage<string> deleteTVS(long id);

        [OperationContract]
        [WebInvoke(Method = "POST",
                                ResponseFormat = WebMessageFormat.Json,
                                RequestFormat = WebMessageFormat.Json,
                               BodyStyle = WebMessageBodyStyle.Bare,
                               UriTemplate = "digital/deleteCategory")]
        ResultMessage<string> deleteCategory(long id);

        [OperationContract]
        [WebInvoke(Method = "POST",
                                ResponseFormat = WebMessageFormat.Json,
                                RequestFormat = WebMessageFormat.Json,
                               BodyStyle = WebMessageBodyStyle.Bare,
                               UriTemplate = "digital/isDirty")]
        ResultMessage<TvsInfoWTO> isDirty(long tv_id);

        [OperationContract]
        [WebInvoke(Method = "POST",
                               ResponseFormat = WebMessageFormat.Json,
                               RequestFormat = WebMessageFormat.Json,
                              BodyStyle = WebMessageBodyStyle.Bare,
                              UriTemplate = "digital/recordSecondPlayed")]
        ResultMessage<Boolean> recordSecondPlayed(long content_ad_id, long tv_id);



        #region Search
        [OperationContract]
        [WebInvoke(Method = "POST",
                               ResponseFormat = WebMessageFormat.Json,
                               RequestFormat = WebMessageFormat.Json,
                              BodyStyle = WebMessageBodyStyle.Bare,
                              UriTemplate = "digital/searchDataOnTvCategories")]
        ResultMessage<List<TvCategoreisInfoWTO>> searchDataOnTvCategories(TvCategoreisInfoWTO filter, PagingInfo paging);

        [OperationContract]
        [WebInvoke(Method = "POST",
                              ResponseFormat = WebMessageFormat.Json,
                              RequestFormat = WebMessageFormat.Json,
                             BodyStyle = WebMessageBodyStyle.Bare,
                             UriTemplate = "digital/searchDataOnTvs")]
        ResultMessage<List<TvsInfoWTO>> searchDataOnTvs(TvsInfoWTO filter, PagingInfo paging);
        [OperationContract]
        [WebInvoke(Method = "POST",
                             ResponseFormat = WebMessageFormat.Json,
                             RequestFormat = WebMessageFormat.Json,
                            BodyStyle = WebMessageBodyStyle.Bare,
                            UriTemplate = "digital/getTvContentWithIp")]
        ResultMessage<string> getTvContentWithIp(string ip ,long companyId);

        [OperationContract]
        [WebInvoke(Method = "POST",
                           ResponseFormat = WebMessageFormat.Json,
                           RequestFormat = WebMessageFormat.Json,
                          BodyStyle = WebMessageBodyStyle.Bare,
                          UriTemplate = "digital/getTvInfo")]
        ResultMessage<List<TvContentsInfoWTO>> getTvInfo(string key);

        #endregion

    }
}
