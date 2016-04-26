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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Ialarms" in both code and config file together.
    [ServiceContract]
    public interface Ialarms
    {
        [OperationContract]

        [WebInvoke(Method = "POST",
                                 ResponseFormat = WebMessageFormat.Json,
                                 RequestFormat = WebMessageFormat.Json,
                                BodyStyle = WebMessageBodyStyle.Bare,
                                UriTemplate = "digital/deleteAlarm")]

        ResultMessage<string> deleteAlarm(long id);

        [OperationContract]

        [WebInvoke(Method = "POST",
                                ResponseFormat = WebMessageFormat.Json,
                                RequestFormat = WebMessageFormat.Json,
                               BodyStyle = WebMessageBodyStyle.Bare,
                               UriTemplate = "digital/saveAlarm")]

        ResultMessage<string> saveAlarm(AlarmsInfoWTO alarms, string tv_ids ,long companyId);


        #region search
        [OperationContract]

        [WebInvoke(Method = "POST",
                              ResponseFormat = WebMessageFormat.Json,
                              RequestFormat = WebMessageFormat.Json,
                             BodyStyle = WebMessageBodyStyle.Bare,
                             UriTemplate = "digital/searchDataOnAlarms")]

        ResultMessage<List<AlarmsInfoWTO>> searchDataOnAlarms(AlarmsInfoWTO filter, PagingInfo paging);


        [OperationContract]

        [WebInvoke(Method = "POST",
                             ResponseFormat = WebMessageFormat.Json,
                             RequestFormat = WebMessageFormat.Json,
                            BodyStyle = WebMessageBodyStyle.Bare,
                            UriTemplate = "digital/loadTvAlarms")]
         ResultMessage<List<AlarmsInfoWTO>> loadTvAlarms(long tvId ,long companyId);
        #endregion
    }
}
