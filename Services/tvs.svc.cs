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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "tvs" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select tvs.svc or tvs.svc.cs at the Solution Explorer and start debugging.
    public class tvs : Itvs
    {
        private string UrlDecode(string s)
        {
            return HttpUtility.UrlDecode(s, System.Text.Encoding.GetEncoding("ISO-8859-1"));
        }

        #region Add
        public ResultMessage<string> addTV(TvsInfoWTO tvsInfo)
        {
            try
            {
                tvsInfo.title = UrlDecode(tvsInfo.title);
                tvsInfo.description = UrlDecode(tvsInfo.description);

                if (tvsInfo.category_id == -1)
                    return new ResultMessage<string>
                    {
                        resultSet = null,
                        result = new Result()
                        {
                            status = Result.state.error,
                            message = "لطفا ایتدا یک گروه را از منو انتخاب نمایید"
                        }
                    };

                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_TVs tv = new DS_TVs();
                tv.title = tvsInfo.title;
                tv.ip = tvsInfo.ip;
                tv.status = (short)tvsInfo.status;
                tv.description = tvsInfo.description;
                tv.category_id = tvsInfo.category_id;
                tv.companyId = tvsInfo.companyId;

                db.DS_TVs.Add(tv);
                db.SaveChanges();

                // Added temporarly
                changeTvContent(tv.id.ToString(), tvsInfo.content_id, "01:01", 20000);

                return new ResultMessage<string>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.success,
                        message = "مانیتور با موفقیت ثبت شد"
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
        public ResultMessage<string> addCategory(TvCategoreisInfoWTO tvCategoreisInfo)
        {
            try
            {
                tvCategoreisInfo.title = UrlDecode(tvCategoreisInfo.title);

                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_TVCategories cat = new DS_TVCategories();
                cat.title = tvCategoreisInfo.title;
                cat.parent_id = tvCategoreisInfo.parent_id;
                cat.status = 1;
                cat.companyId = tvCategoreisInfo.companyId;

                db.DS_TVCategories.Add(cat);
                db.SaveChanges();

                return new ResultMessage<string>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.success,
                        message = "گروه با موفقیت ثبت شد"
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


        #endregion

        #region Edit
        public ResultMessage<string> editTV(TvsInfoWTO tvsInfo)
        {
            try
            {
                tvsInfo.title = UrlDecode(tvsInfo.title);
                tvsInfo.description = UrlDecode(tvsInfo.description);

                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_TVs tv = (from t in db.DS_TVs where t.id == tvsInfo.id select t).FirstOrDefault();
                tv.title = tvsInfo.title;
                tv.ip = tvsInfo.ip;
                tv.status = (short)tvsInfo.status;
                tv.description = tvsInfo.description;
                tv.category_id = tvsInfo.category_id;
                tv.companyId = tvsInfo.companyId;

                db.SaveChanges();

                // Added temporarly
                changeTvContent(tv.id.ToString(), tvsInfo.content_id, "01:01", 20000);

                return new ResultMessage<string>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.success,
                        message = "مانیتور با موفقیت ویرایش شد",
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
        public ResultMessage<string> changeTvContent(string tv_ids, long content_id, String startTime, int minute)
        {
            try
            {
                tv_ids = UrlDecode(tv_ids);
                startTime = UrlDecode(startTime);

                int startHour = Convert.ToInt32(startTime.Split(':')[0]);
                int startMinute = Convert.ToInt32(startTime.Split(':')[1]);
                int endHour = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, startHour, startMinute, 0)).AddMinutes(minute).Hour;
                int endMinute = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, startHour, startMinute, 0)).AddMinutes(minute).Minute;
                DigitalSignageEntities db = new DigitalSignageEntities();

                foreach (String tv_id in tv_ids.Split(','))
                {
                    if (!string.IsNullOrWhiteSpace(tv_id))
                    {
                        DS_TVContents tvContent = (from t in db.DS_TVContents where t.tv_id == Convert.ToInt32(tv_id) select t).FirstOrDefault();
                        if (tvContent == null)
                        {
                            tvContent = new DS_TVContents();
                            tvContent.content_id = content_id;
                            tvContent.startTime = Convert.ToInt32("1" + startHour.ToString().PadLeft(2, '0') + startMinute.ToString().PadLeft(2, '0'));
                            tvContent.endTime = Convert.ToInt32("1" + endHour.ToString().PadLeft(2, '0') + endMinute.ToString().PadLeft(2, '0'));
                            tvContent.duration = minute;
                            tvContent.status = 1;
                            tvContent.type = 1;
                            tvContent.tv_id = Convert.ToInt32(tv_id);
                            tvContent.content_id = content_id;
                            db.DS_TVContents.Add(tvContent);
                        }
                        else
                        {
                            tvContent.content_id = content_id;
                            tvContent.startTime = Convert.ToInt32("1" + startHour.ToString().PadLeft(2, '0') + startMinute.ToString().PadLeft(2, '0'));
                            tvContent.endTime = Convert.ToInt32("1" + endHour.ToString().PadLeft(2, '0') + endMinute.ToString().PadLeft(2, '0'));
                            tvContent.duration = minute;
                            tvContent.status = 1;
                            tvContent.type = 1;
                            tvContent.tv_id = Convert.ToInt32(tv_id);
                            tvContent.content_id = content_id;
                        }

                        // Make tv dirty to refresh it's content
                        DS_TVs tv = (from t in db.DS_TVs where t.id == Convert.ToInt32(tv_id) select t).FirstOrDefault();
                        if (tv != null)
                            tv.isDirty = 1;

                        db.SaveChanges();
                    }
                }

                return new ResultMessage<string>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        message = "با موفقیت ذخیره شد",
                        status = Result.state.success,
                        redirectUrl = ""
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

        public ResultMessage<TvsInfoWTO> isDirty(long tv_id)
        {
            try
            {
                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_TVs tv = (from t in db.DS_TVs where t.id == tv_id select t).FirstOrDefault();

                //int currentTime = Convert.ToInt32("1" + DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0'));
                //DS_TVContents content = (from c in db.DS_TVContents where c.tv_id == tv.id && c.startTime < currentTime && c.endTime >= currentTime select c).FirstOrDefault();

                if (tv != null)
                {

                    TvsInfoWTO tvs = new TvsInfoWTO();
                    tvs.content_id = (int)tv.DS_TVContents.FirstOrDefault().content_id;
                    tvs.lastAlive = (long)tv.lastAlive;
                    if (tv.isDirty == 1)
                    {
                        tvs.isDirty = 1;

                        tv.lastAlive = Aryaban.Engine.Core.Utilities.DateUtils.getCurrentPersianDateTimeAsNumber();
                        tv.isDirty = 0;
                        db.SaveChanges();

                        return new ResultMessage<TvsInfoWTO>
                        {
                            resultSet = tvs,
                            result = new Result()
                            {
                                status = Result.state.success,
                            }
                        };
                    }

                    else
                    {
                        tvs.isDirty = 0;
                        return new ResultMessage<TvsInfoWTO>
                        {
                            resultSet = tvs,
                            result = new Result()
                            {
                                status = Result.state.success,

                            }

                        };
                    }

                }

                return new ResultMessage<TvsInfoWTO>
                {

                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.success,

                    }
                };
            }

            catch (Exception ex)
            {
                return new ResultMessage<TvsInfoWTO>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.error,
                        message = ex.Message,

                    }
                };
            }
        }

        public ResultMessage<Boolean> recordSecondPlayed(long content_ad_id, long tv_id)
        {
            try
            {
                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_ContentAds contentAd = (from t in db.DS_ContentAds where t.id == content_ad_id select t).FirstOrDefault();
                if (contentAd != null)
                {
                    DS_TVContentAds tv_contentAd = new DS_TVContentAds();
                    tv_contentAd.content_ad_id = content_ad_id;
                    tv_contentAd.tv_id = tv_id;
                    tv_contentAd.datetime = Aryaban.Engine.Core.Utilities.DateUtils.getCurrentPersianDateTimeAsNumber();
                    db.DS_TVContentAds.Add(tv_contentAd);
                    db.SaveChanges();

                    if (contentAd.secondViewed == null)
                        contentAd.secondViewed = 0;
                    if (contentAd.DS_Ads.passed_minutes == null)
                        contentAd.DS_Ads.passed_minutes = 0;
                    contentAd.secondViewed += 1;
                    contentAd.DS_Ads.passed_minutes += 1;
                    db.SaveChanges();

                    return new ResultMessage<Boolean>
                    {
                        resultSet = true,
                        result = new Result()
                        {
                            status = Result.state.success,
                            callback = string.Format("checkContentAdSecondRemained({0},{1});", content_ad_id, (contentAd.DS_Ads.passed_minutes >= contentAd.DS_Ads.max_minutes).ToString().ToLower()),
                            redirectUrl = "none"
                        }
                    };
                }

                return new ResultMessage<Boolean>
                {
                    resultSet = false,
                    result = new Result()
                    {
                        status = Result.state.error,
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResultMessage<Boolean>
                {
                    resultSet = false,
                    result = new Result()
                    {
                        status = Result.state.error,
                        message = ex.Message,
                    }
                };
            }
        }
        #endregion

        #region Delete
        public ResultMessage<string> deleteCategory(long id)
        {
            try
            {
                DigitalSignageEntities db = new DigitalSignageEntities();
                var item = (from i in db.DS_TVCategories where i.parent_id == 0 select i);
                if (item.Count() == 1)
                {
                    if (item.Where(p => p.id == id).Any())
                    {
                        return new ResultMessage<string>
                        {
                            resultSet = null,
                            result = new Result()
                            {
                                status = Result.state.error,
                                message = "حذف گروه فوق ممکن نیست"
                            }
                        };
                    }
                    else
                    {

                        DS_TVCategories cat = (from c in db.DS_TVCategories where c.id == id select c).FirstOrDefault();
                        if (cat != null)
                        {

                            db.DS_TVCategories.Remove(cat);
                            db.SaveChanges();

                        }

                        return new ResultMessage<string>
                        {
                            resultSet = null,
                            result = new Result()
                            {
                                status = Result.state.success,
                                message = "گروه با موفقیت حذف شد"
                            }
                        };
                    }
                }
                else
                {

                    DS_TVCategories cat = (from c in db.DS_TVCategories where c.id == id select c).FirstOrDefault();
                    if (cat != null)
                    {

                        db.DS_TVCategories.Remove(cat);
                        db.SaveChanges();

                    }

                    return new ResultMessage<string>
                    {
                        resultSet = null,
                        result = new Result()
                        {
                            status = Result.state.success,
                            message = "گروه با موفقیت حذف شد"
                        }
                    };
                }
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
        public ResultMessage<string> deleteTVS(long id)
        {
            try
            {
                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_TVs tvs = (from c in db.DS_TVs where c.id == id select c).FirstOrDefault();
                if (tvs != null)
                {
                    db.DS_TVs.Remove(tvs);
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
        #endregion

        #region Search
        public ResultMessage<List<TvCategoreisInfoWTO>> searchDataOnTvCategories(TvCategoreisInfoWTO filter, PagingInfo paging)
        {
            try
            {
                GetDataManager<TvCategoreisInfoWTO, DS_TVCategories> entitySearch = new GetDataManager<TvCategoreisInfoWTO, DS_TVCategories>();
                List<DS_TVCategories> lst = entitySearch.getGeneralSearch(filter, paging, "DS_TVCategories");
                List<TvCategoreisInfoWTO> listSearch = entitySearch.MapFrom(lst);
                return new ResultMessage<List<TvCategoreisInfoWTO>>
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
                return new ResultMessage<List<TvCategoreisInfoWTO>>
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
        public ResultMessage<List<TvsInfoWTO>> searchDataOnTvs(TvsInfoWTO filter, PagingInfo paging)
        {
            try
            {
                GetDataManager<TvsInfoWTO, DS_TVs> entitySearch = new GetDataManager<TvsInfoWTO, DS_TVs>();
                List<DS_TVs> lst = entitySearch.getGeneralSearch(filter, paging, "DS_TVs");
                List<TvsInfoWTO> listSearch = entitySearch.MapFrom(lst);
                return new ResultMessage<List<TvsInfoWTO>>
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
                return new ResultMessage<List<TvsInfoWTO>>
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

        public ResultMessage<List<TvContentsInfoWTO>> getTvInfo(string key)
        {
            try
            {
                List<TvContentsInfoWTO> listSearch = new List<TvContentsInfoWTO>();
                DigitalSignageEntities db = new DigitalSignageEntities();


                var Tvitem = (from i in db.DS_TVs
                              join j in db.DS_TVContents on i.id equals j.tv_id
                              where i.identifyKey == key 
                              select new { i, j.tv_id, j.content_id, j.startTime, j.endTime }).ToList();

                foreach (var item in Tvitem)
                {
                    TvContentsInfoWTO newItem = new TvContentsInfoWTO();
                    newItem.companyId = (long)item.i.companyId;
                    newItem.tv_id = (long)item.tv_id;
                    newItem.content_id = (long)item.content_id;
                    newItem.startTime = (long)item.startTime;
                    newItem.endTime = (long)item.endTime;
                   // newItem.x = (int)item.i.x;
                   // newItem.y = (int)item.i.y;
                    newItem.lastAlive = (long)item.i.lastAlive;
                    newItem.status = (short)item.i.status;
                    newItem.isDirty = (short)item.i.isDirty;
                    listSearch.Add(newItem);

                }

                return new ResultMessage<List<TvContentsInfoWTO>>
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
                return new ResultMessage<List<TvContentsInfoWTO>>
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

        public ResultMessage<string> getTvContentWithIp(string ip, long companyId)
        {
            try
            {
                List<ContentInfoWTO> listSearch = new List<ContentInfoWTO>();
                DigitalSignageEntities db = new DigitalSignageEntities();
                var Tvitem = (from t in db.DS_TVs where t.ip == ip && t.companyId == companyId select t).FirstOrDefault();
                if (Tvitem != null)
                {
                    DS_TVContents content = (from c in db.DS_TVContents where c.tv_id == Tvitem.id && c.companyId == companyId select c).FirstOrDefault();
                    if (content != null)
                    {
                        return new ResultMessage<string>
                        {
                            resultSet = null,
                            result = new Result()
                            {
                                status = Result.state.success,
                            }
                        };
                    }
                }
                return new ResultMessage<string>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.success,
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


        #endregion


    }
}
