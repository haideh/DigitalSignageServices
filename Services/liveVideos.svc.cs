using Aryaban.Engine.Core.WebService;
using DigitalServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;

namespace DigitalServices.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "liveVideos" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select liveVideos.svc or liveVideos.svc.cs at the Solution Explorer and start debugging.
    public class liveVideos : IliveVideos
    {
        public ResultMessage<string> addLiveVideo(LiveTVInfoWTO videos)
        {
            try
            {
                //com.magfa.sms.
                XDocument xdoc = XDocument.Load("http://www.videoplayer.ir/epg.xml");

                //Run query
                var allNodes = from lv1 in xdoc.Descendants("programme")
                               select new
                               {
                                   channel = lv1.Attribute("channel").Value.Substring(0, lv1.Attribute("channel").Value.IndexOf(".")),
                                   startDate = lv1.Attribute("start").Value.Substring(0, lv1.Attribute("start").Value.IndexOf(" ")),
                                   endDate = lv1.Attribute("stop").Value.Substring(0, lv1.Attribute("stop").Value.IndexOf(" ")),
                                   title = lv1.Descendants("title").FirstOrDefault().Value,
                                   description = lv1.Descendants("desc").FirstOrDefault().Value
                               };

                int iUpdated = 0;
                DigitalSignageEntities db = new DigitalSignageEntities();
                foreach (var node in allNodes)
                {
                    DS_LivePrograms program = (from p in db.DS_LivePrograms where p.startDate == dateTime(Convert.ToInt64(node.startDate)) && p.endDate == dateTime(Convert.ToInt64(node.endDate)) && p.DS_Lives.channel == node.channel && p.companyId==videos.companyId select p).FirstOrDefault();
                    if (program == null)
                    {
                        iUpdated++;
                        DS_Lives live = (from l in db.DS_Lives where l.channel == node.channel   select l).FirstOrDefault();
                        if (live != null)
                        {
                            program = new DS_LivePrograms();
                            program.startDate = dateTime(Convert.ToInt64(node.startDate));
                            program.endDate = dateTime(Convert.ToInt64(node.endDate));
                            program.title = node.title;
                            program.description = node.description;
                            program.live_id = live.id;
                            db.DS_LivePrograms.Add(program);
                            db.SaveChanges();
                        }
                    }
                }

                return new ResultMessage<string>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.success,
                        message = "ویديو با موفقیت ثبت شد",
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResultMessage<string>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.error,
                        message = ex.Message
                    }
                };
            }
        }

        private long dateTime(long datetime)
        {
            DateTime dt = DateTime.Parse(Aryaban.Engine.Core.Utilities.DateUtils.getDateTime(datetime));
            dt = dt.AddHours(-3);
            dt = dt.AddMinutes(-30);
            return Convert.ToInt64(string.Format("{0}{1}{2}{3}{4}{5}", dt.Year, dt.Month.ToString().PadLeft(2, '0'), dt.Day.ToString().PadLeft(2, '0'), dt.Hour.ToString().PadLeft(2, '0'), dt.Minute.ToString().PadLeft(2, '0'), dt.Second.ToString().PadLeft(2, '0')));

        }
    }
}
