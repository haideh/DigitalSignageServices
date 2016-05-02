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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Ipermission" in both code and config file together.
    [ServiceContract]
    public interface Ipermission
    {
        [OperationContract]

        [WebInvoke(Method = "POST",
                                 ResponseFormat = WebMessageFormat.Json,
                                 RequestFormat = WebMessageFormat.Json,
                                BodyStyle = WebMessageBodyStyle.Bare,
                                UriTemplate = "digital/login")]

        ResultMessage<UserInfoWTO> login(UserInfoWTO userInfo);

        [OperationContract]

        [WebInvoke(Method = "POST",
                               ResponseFormat = WebMessageFormat.Json,
                               RequestFormat = WebMessageFormat.Json,
                              BodyStyle = WebMessageBodyStyle.Bare,
                              UriTemplate = "digital/signup")]

        ResultMessage<UserInfoWTO> signup(UserInfoWTO userInfo);

        [OperationContract]

        [WebInvoke(Method = "POST",
                               ResponseFormat = WebMessageFormat.Json,
                               RequestFormat = WebMessageFormat.Json,
                              BodyStyle = WebMessageBodyStyle.Bare,
                              UriTemplate = "digital/ChangePassword")]

        ResultMessage<bool> ChangePassword(long id, string OldPass, string NewPass, string ReNewPass);
    }
}
