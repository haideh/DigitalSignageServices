using Aryaban.Engine.Core.WebService;
using DigitalServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;

namespace DigitalServices.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "alarms" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select alarms.svc or alarms.svc.cs at the Solution Explorer and start debugging.
    public class alarms : Ialarms
    {
        private string UrlDecode(string s)
        {
            return HttpUtility.UrlDecode(s, System.Text.Encoding.GetEncoding("ISO-8859-1"));
        }

        public ResultMessage<string> deleteAlarm(long id)
        {
            try
            {
                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_Alarms alarm = null;
                alarm = (from a in db.DS_Alarms where a.id == id select a).FirstOrDefault();
                if (alarm != null)
                {
                    db.DS_Alarms.Remove(alarm);
                    db.SaveChanges();
                }


                return new ResultMessage<string>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.success,
                        message = "حذف با موفقیت انجام شد"
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

        public ResultMessage<string> saveAlarm(AlarmsInfoWTO alarms, string tv_ids , long companyId)
        {
            try
            {
                alarms.message = UrlDecode(alarms.message);
                tv_ids = UrlDecode(tv_ids);

                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_Alarms alarm = null;

                if (!string.IsNullOrWhiteSpace(alarms.id))
                {
                    alarm = (from a in db.DS_Alarms where a.id == Convert.ToInt32(alarms.id) select a).FirstOrDefault();
                    if (alarm != null)
                    {
                        alarm.message = alarms.message;
                        alarm.type = (short)alarms.type;
                        alarm.status = 1;
                        alarm.companyId = companyId;
                    }
                }
                else
                {
                    alarm = new DS_Alarms();
                    alarm.message = alarms.message;
                    alarm.type = (short)alarms.type;
                    alarm.status = 1;
                    alarm.companyId = companyId;
                    db.DS_Alarms.Add(alarm);
                }

                db.SaveChanges();

                // Remove isDirty for other tvs
                var tvs = (from t in db.DS_TVs where t.alarm_id == Convert.ToInt32(alarms.id) select t).ToList();
                foreach (var tv in tvs)
                {
                    tv.isDirty = 1;
                    tv.alarm_id = null;
                }
                db.SaveChanges();


                // Add tv relation
                if (tv_ids != "-")
                {
                    foreach (var tv_id in tv_ids.Split(','))
                    {
                        if (!string.IsNullOrWhiteSpace(tv_id))
                        {
                            DS_TVs tv = (from t in db.DS_TVs where t.id == Convert.ToInt32(tv_id) select t).FirstOrDefault();
                            if (tv != null)
                            {
                                tv.alarm_id = alarm.id;
                                tv.isDirty = 1;
                            }
                        }
                    }
                }

                db.SaveChanges();

                return new ResultMessage<string>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.success,
                        message = "پیام اضطراری با موفقیت ذخیره شد",
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

        #region Search
        //Generic
        public ResultMessage<List<AlarmsInfoWTO>> searchDataOnAlarms(AlarmsInfoWTO filter, PagingInfo paging)
        {
            try
            {
                GetDataManager<AlarmsInfoWTO, DS_Alarms> entitySearch = new GetDataManager<AlarmsInfoWTO, DS_Alarms>();
                List<DS_Alarms> lst = entitySearch.getGeneralSearch(filter, paging, "DS_Alarms");
                List<AlarmsInfoWTO> listSearch = entitySearch.MapFrom(lst);
                return new ResultMessage<List<AlarmsInfoWTO>>
                {
                    resultSet = listSearch,
                    result = new Result()
                    {
                        status = Result.state.success,
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResultMessage<List<AlarmsInfoWTO>>
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

        public ResultMessage<List<AlarmsInfoWTO>> loadTvAlarms(long tvId , long companyId)
        {
            try
            {

                List<AlarmsInfoWTO> listSearch = new List<AlarmsInfoWTO>();
                DigitalSignageEntities db = new DigitalSignageEntities();
                var additemLis = (from i in db.DS_Alarms
                                  join j in db.DS_TVs on i.id equals j.alarm_id
                                  where j.id== tvId && j.companyId== companyId
                                  select i).ToList();
                foreach (var item in additemLis)
                {
                    AlarmsInfoWTO newItem = new AlarmsInfoWTO();
                    newItem.message = item.message;
                    newItem.id = item.id.ToString();
                    newItem.type = (int)item.type;
                    newItem.status = (int)item.status;
                    newItem.companyId = companyId;
                
                    listSearch.Add(newItem);
                }

                return new ResultMessage<List<AlarmsInfoWTO>>
                {
                    resultSet = listSearch,
                    result = new Result()
                    {
                        status = Result.state.success,
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResultMessage<List<AlarmsInfoWTO>>
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
      
        #endregion
    }
}
