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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ads" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ads.svc or ads.svc.cs at the Solution Explorer and start debugging.
    public class ads : Iads
    {
        private string UrlDecode(string s)
        {
            return HttpUtility.UrlDecode(s, Encoding.GetEncoding("ISO-8859-1"));
        }

        #region Add
        public ResultMessage<string> saveAds(AdsInfoWTO AdsInfo)
        {
            try
            {
                bool result;
                AdsInfo.title = UrlDecode(AdsInfo.title);
                AdsInfo.adItemImageFileName = UrlDecode(AdsInfo.adItemImageFileName);
                AdsInfo.adItemVedioFileName = UrlDecode(AdsInfo.adItemVedioFileName);

                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_Ads ad = new DS_Ads();
                ad.title = AdsInfo.title;
                ad.max_minutes = AdsInfo.max_minutes;
                ad.passed_minutes = 0;
                ad.type = (short)AdsInfo.type;
                ad.companyId = AdsInfo.companyId;

                db.DS_Ads.Add(ad);
                db.SaveChanges();

                if (AdsInfo.type == 1)
                    foreach (string image in AdsInfo.adItemImageFileName.Split(','))
                        if (!string.IsNullOrWhiteSpace(image))
                            result = saveAdItemImage(image, (long)ad.id, AdsInfo.companyId);

                if (AdsInfo.type == 2)
                    foreach (string video in AdsInfo.adItemVedioFileName.Split(','))
                        if (!string.IsNullOrWhiteSpace(video))
                            saveAdItemVideo(video, 0, (long)ad.id , AdsInfo.companyId);

                if (AdsInfo.type == 3)
                    saveAdItemText(AdsInfo.adItemTitle, AdsInfo.adItemDesc, (long)ad.id, AdsInfo.companyId);

                if (AdsInfo.type == 4)
                    saveAdItemFeed(AdsInfo.adItemFeedUrl, AdsInfo.adItemFeedTitle, (long)ad.id, AdsInfo.companyId);
                return new ResultMessage<string>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.success,
                        message = "تبلیغ با موفقیت ثبت شد",
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

        private bool saveAdItemImage(string file_name, long ad_id, long companyId)
        {
            try
            {
                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_AdItems ad = new DS_AdItems();
                ad.ad_id = ad_id;
                ad.file_name = file_name;
                ad.companyId = companyId;

                if (System.IO.File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + "/modules/DigitalSignage/data/uploader/" + file_name))
                {
                    System.IO.File.Copy(HttpContext.Current.Request.PhysicalApplicationPath + "/modules/DigitalSignage/data/uploader/" + file_name, HttpContext.Current.Request.PhysicalApplicationPath + "/modules/DigitalSignage/data/ads/images/" + file_name);
                    try
                    {
                        System.IO.File.Delete(HttpContext.Current.Request.PhysicalApplicationPath + "/modules/DigitalSignage/data/uploader/" + file_name);
                    }
                    catch (Exception)
                    {
                    }

                }

                db.DS_AdItems.Add(ad);
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool saveAdItemVideo(string file_name, int live_id, long ad_id, long companyId)
        {
            try
            {

                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_AdItems ad = new DS_AdItems();
                ad.ad_id = ad_id;
                ad.file_name = file_name;
                ad.companyId = companyId;

                if (System.IO.File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + "/modules/DigitalSignage/data/uploader/" + file_name))
                {
                    System.IO.File.Copy(HttpContext.Current.Request.PhysicalApplicationPath + "/modules/DigitalSignage/data/uploader/" + file_name, HttpContext.Current.Request.PhysicalApplicationPath + "/modules/DigitalSignage/data/ads/movies/" + file_name.Replace(".jpg", ".mp4"));
                    try
                    {
                        System.IO.File.Delete(HttpContext.Current.Request.PhysicalApplicationPath + "/modules/DigitalSignage/data/uploader/" + file_name);
                    }
                    catch (Exception)
                    {
                    }
                    ad.file_name = file_name.Replace(".jpg", ".mp4");
                }

                db.DS_AdItems.Add(ad);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool saveAdItemText(string title, string body, long ad_id, long companyId)
        {
            try
            {
                title = UrlDecode(title);
                body = UrlDecode(body);
                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_AdItems ad = new DS_AdItems();
                ad.ad_id = ad_id;
                ad.title = title;
                ad.description = body;
                ad.companyId = companyId;

                db.DS_AdItems.Add(ad);
                db.SaveChanges();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool saveAdItemFeed(string url, string title, long ad_id, long companyId)
        {
            try
            {
                title = UrlDecode(title);
                url = UrlDecode(url);

                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_AdItems ad = new DS_AdItems();
                ad.ad_id = ad_id;
                ad.title = title;
                ad.description = url;
                ad.companyId = companyId;
                db.DS_AdItems.Add(ad);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region Delete
        public ResultMessage<string> deleteAds(long id)
        {
            try
            {
                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_Ads ad = (from a in db.DS_Ads where a.id == id select a).FirstOrDefault();
                if (ad != null)
                {
                    db.DS_Ads.Remove(ad);
                    db.SaveChanges();
                }

                return new ResultMessage<string>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.success,
                        message = "تبلیغ با موفقیت حذف شد",
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

        public ResultMessage<string> deleteAdsItem(long id)
        {
            try
            {
                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_AdItems ad = (from a in db.DS_AdItems where a.id == id select a).FirstOrDefault();
                if (ad != null)
                {
                    db.DS_AdItems.Remove(ad);
                    db.SaveChanges();
                }

                return new ResultMessage<string>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.success,
                        message = "حذف با موفقیت انجام شد",
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
        public ResultMessage<string> editAds(string title, string time, long ad_id)
        {
            try
            {
                title = UrlDecode(title);
                time = UrlDecode(time);

                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_Ads ad = (from a in db.DS_Ads where a.id == ad_id select a).FirstOrDefault();
                if (ad != null)
                {
                    ad.title = title;
                    ad.max_minutes = Convert.ToInt32(time);
                    db.SaveChanges();
                }

                return new ResultMessage<string>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.success,
                        message = "تبلیغ با موفقیت ویرایش شد",
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
        //Generic
        public ResultMessage<List<AdsInfoWTO>> searchDataOnAds(AdsInfoWTO filter, PagingInfo paging)
        {

            try
            {
                GetDataManager<AdsInfoWTO, DS_Ads> entitySearch = new GetDataManager<AdsInfoWTO, DS_Ads>();
                List<DS_Ads> lst = entitySearch.getGeneralSearch(filter, paging, "DS_Ads");
                List<AdsInfoWTO> listSearch = entitySearch.MapFrom(lst);
                return new ResultMessage<List<AdsInfoWTO>>
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
                return new ResultMessage<List<AdsInfoWTO>>
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

        //Generic
        public ResultMessage<List<AdsIemInfoWTO>> searchDataOnAdsItem(AdsIemInfoWTO filter, PagingInfo paging)
        {
            try
            {
                GetDataManager<AdsIemInfoWTO, DS_AdItems> entitySearch = new GetDataManager<AdsIemInfoWTO, DS_AdItems>();
                List<DS_AdItems> lst = entitySearch.getGeneralSearch(filter, paging, "DS_AdItems");
                List<AdsIemInfoWTO> listSearch = entitySearch.MapFrom(lst);
                return new ResultMessage<List<AdsIemInfoWTO>>
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
                return new ResultMessage<List<AdsIemInfoWTO>>
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

        //Generic
        public ResultMessage<List<LiveTVInfoWTO>> searchDataOnTVLives(LiveTVInfoWTO filter, PagingInfo paging)
        {
            try
            {
                GetDataManager<LiveTVInfoWTO, DS_Lives> entitySearch = new GetDataManager<LiveTVInfoWTO, DS_Lives>();
                List<DS_Lives> lst = entitySearch.getGeneralSearch(filter, paging, "DS_Lives");
                List<LiveTVInfoWTO> listSearch = entitySearch.MapFrom(lst);
                return new ResultMessage<List<LiveTVInfoWTO>>
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
                return new ResultMessage<List<LiveTVInfoWTO>>
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

        public ResultMessage<List<AdsInfoWTO>> getAdsWithItemDetail(string type , long companyId, long content_id, int position)
        {
            try
            {
                List<AdsInfoWTO> listSearch = new List<AdsInfoWTO>();
                DigitalSignageEntities db = new DigitalSignageEntities();
                short typeAds = Convert.ToInt16(type);
                var contentItemList = (from i in db.DS_ContentAds where i.content_id == content_id && i.position == position && i.live_id==null select i.ad_id);

                var additemLis = (from i in db.DS_Ads where i.type == typeAds && i.companyId == companyId   && !contentItemList.Contains(i.id) select i).ToList();
                foreach (var item in additemLis)
                {
                    AdsInfoWTO newItem = new AdsInfoWTO();
                    newItem.title = item.title;
                    newItem.max_minutes = (int)item.max_minutes;
                    newItem.id = (long)item.id;
                    newItem.type = (int)item.type;
                    newItem.companyId = companyId;
                    newItem.itemList = new List<AdsIemInfoWTO>();
                    var adsDetail = (from j in db.DS_AdItems where j.ad_id == item.id select j).ToList();
                    foreach (var detail in adsDetail)
                    {
                        AdsIemInfoWTO newAdsDetail = new AdsIemInfoWTO();
                        newAdsDetail.title = detail.title;
                        newAdsDetail.file_name = detail.file_name;
                        newAdsDetail.id = (long)detail.id;
                        newAdsDetail.description = detail.description;
                        newAdsDetail.companyId = companyId;
                        newItem.itemList.Add(newAdsDetail);
                    }
                    listSearch.Add(newItem);
                }

                return new ResultMessage<List<AdsInfoWTO>>
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
                return new ResultMessage<List<AdsInfoWTO>>
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
        public ResultMessage<List<AdsInfoWTO>> getWidgetAdsWithItemDetail(long companyId)
        {
            try
            {
                List<AdsInfoWTO> listSearch = new List<AdsInfoWTO>();
                DigitalSignageEntities db = new DigitalSignageEntities();
                var additemLis = (from i in db.DS_Ads where (i.type == 4 || i.type==5) && (i.companyId == companyId || i.companyId == null) select i).ToList();
                foreach (var item in additemLis)
                {
                    AdsInfoWTO newItem = new AdsInfoWTO();
                    newItem.title = item.title;
                    newItem.max_minutes = (int)item.max_minutes;
                    newItem.id = (long)item.id;
                    newItem.type = (int)item.type;
                    newItem.companyId = companyId;
                    newItem.itemList = new List<AdsIemInfoWTO>();
                    var adsDetail = (from j in db.DS_AdItems where j.ad_id == item.id select j).ToList();
                    foreach (var detail in adsDetail)
                    {
                        AdsIemInfoWTO newAdsDetail = new AdsIemInfoWTO();
                        newAdsDetail.title = detail.title;
                        newAdsDetail.file_name = detail.file_name;
                        newAdsDetail.id = (long)detail.id;
                        newAdsDetail.description = detail.description;
                        newAdsDetail.companyId = companyId;
                        newItem.itemList.Add(newAdsDetail);
                    }
                    listSearch.Add(newItem);
                }

                return new ResultMessage<List<AdsInfoWTO>>
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
                return new ResultMessage<List<AdsInfoWTO>>
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
