using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;

namespace DigitalServices.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "weather" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select weather.svc or weather.svc.cs at the Solution Explorer and start debugging.
    public class weather : Iweather
    {
        private string UrlDecode(string s)
        {
            return HttpUtility.UrlDecode(s, System.Text.Encoding.GetEncoding("ISO-8859-1"));
        }

        public void getWeatherInfo()
        {
            try
            {
                string urls = "http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20location%20in%20(%27IRXX0018%27)%20and%20u%3D%27c%27&rnd=20159211&format=json&callback=jQuery19107740621459670365_1445932075067&_=1445932075068";
                urls = UrlDecode(urls);
                // string data = Ariaban.Parser.HTML.HttpRequestAPI.getInstance().get(urls);

                var req = System.Net.HttpWebRequest.Create(urls);
                req.ContentType = "application/json; charset=utf-8";
                req.Method = "GET";
                var response = req.GetResponse();
                System.IO.Stream dataStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                string data = reader.ReadToEnd();
                //System.Web.Script.Serialization.JavaScriptSerializer oserializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                //string dJSON = oserializer.Serialize(data);
                //return dJSON;
                // return data;

                data = data.Replace("/**/jQuery19107740621459670365_1445932075067(", "");
                data = data.Replace("}}}}}});", "}}}}}}");

                System.IO.File.WriteAllText(HttpContext.Current.Request.PhysicalApplicationPath + "/modules/DigitalSignage/data/weather.txt", data);
                //System.IO.File.WriteAllText(HttpContext.Current.Request.PhysicalApplicationPath+"file.txt", data);
                downlaod(data);

                System.Web.HttpContext.Current.Response.Write(data);
            }
            catch
            {// return ""; 
            }

        }
        public void downlaod(string jHtml)
        {
            try
            {
                JObject jobject = (JObject)JsonConvert.DeserializeObject(jHtml);

                string code = jobject.SelectToken("query").SelectToken("results").SelectToken("channel").SelectToken("item").SelectToken("condition").SelectToken("code").ToString();
                string date = jobject.SelectToken("query").SelectToken("results").SelectToken("channel").SelectToken("item").SelectToken("pubDate").ToString();
                date = date.Substring(date.IndexOf(",") + 2, date.Length - (date.IndexOf(",") + 2));
                date = date.Replace(" IRT", "");
                DateTime dt = DateTime.Parse(date);
                string TimeNow = dt.TimeOfDay.ToString();

                string sunrise = jobject.SelectToken("query").SelectToken("results").SelectToken("channel").SelectToken("astronomy").SelectToken("sunrise").ToString();
                string sunset = jobject.SelectToken("query").SelectToken("results").SelectToken("channel").SelectToken("astronomy").SelectToken("sunset").ToString();
                DateTime.Parse(sunrise);
                string sunr = dt.TimeOfDay.ToString();
                DateTime.Parse(sunset);
                string suns = dt.TimeOfDay.ToString();
                string daynight;
                if (Convert.ToInt32(TimeNow.Replace(":", "")) > Convert.ToInt32(sunr.Replace(":", "")) && Convert.ToInt32(TimeNow.Replace(":", "")) < Convert.ToInt32(suns.Replace(":", ""))) { daynight = "d"; } else { daynight = "n"; }

                Ariaban.Parser.HTML.HttpRequestAPI.getInstance().downloader("http://l.yimg.com/a/i/us/nws/weather/gr/" + code + daynight + ".png", HttpContext.Current.Request.PhysicalApplicationPath + "/modules/DigitalSignage/data/", "weather.png");


            }
            catch
            {
                Ariaban.Parser.HTML.HttpRequestAPI.getInstance().downloader("http://l.yimg.com/a/i/us/nws/weather/gr/28d.png", HttpContext.Current.Request.PhysicalApplicationPath + "/modules/DigitalSignage/data/", "weather.png");
            }
        }

    }
}

