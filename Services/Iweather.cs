using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Services;

namespace DigitalServices.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Iweather" in both code and config file together.
    [ServiceContract]
    public interface Iweather
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
                                   ResponseFormat = WebMessageFormat.Json,
                                   RequestFormat = WebMessageFormat.Json,
                                  BodyStyle = WebMessageBodyStyle.Bare,
                                  UriTemplate = "digital/getWeatherInfo")]
        void getWeatherInfo();

    }
}
