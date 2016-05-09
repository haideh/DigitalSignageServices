using Aryaban.Engine.Core.WebService;
using DigitalServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using PagedList;
using Aryaban.Engine.Core.Utilities;

namespace DigitalServices.Services
{

    public class contents : Icontents
    {
        private string UrlDecode(string s)
        {
            return HttpUtility.UrlDecode(s, System.Text.Encoding.GetEncoding("ISO-8859-1"));
        }


        #region Add
        public ResultMessage<string> addContent(ContentInfoWTO contentInfo)
        {
            try
            {
                contentInfo.title = UrlDecode(contentInfo.title);
                contentInfo.description = UrlDecode(contentInfo.description);


                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_Contents content = (from c in db.DS_Contents where c.id == contentInfo.content_id select c).FirstOrDefault();
                if (content == null)
                    throw new Exception("Content was not created");
                content.title = contentInfo.title;
                content.description = contentInfo.description;
                content.contentType_id = contentInfo.content_id;
                content.status = 1;
                content.companyId = contentInfo.companyId;

                db.SaveChanges();

                return new ResultMessage<string>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.success,
                        message = "محتوا با موفقیت ثبت شد"
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
        public ResultMessage<int> getNewContentId()
        {
            try
            {
                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_Contents content = new DS_Contents();
                content.status = 0;

                db.DS_Contents.Add(content);
                db.SaveChanges();

                return new ResultMessage<int>
                {
                    resultSet = (int)content.id,
                    result = new Result()
                    {
                        status = Result.state.success,
                        message = "محتوا با موفقیت ثبت شد"
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResultMessage<int>
                {
                    resultSet = 0,
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
        public ResultMessage<string> editContentOption(ContentOptionInfoWTO contentOptionInfo)
        {
            try
            {
                DigitalSignageEntities db = new DigitalSignageEntities();
                // Update minute and shuffle for all package at this position
                var contents = (from c in db.DS_ContentAds where c.content_id == contentOptionInfo.content_id && c.position == (short)contentOptionInfo.position select c).ToList();
                foreach (var c in contents)
                {
                    c.interval = contentOptionInfo.second;
                    c.shuffle = (byte)contentOptionInfo.shuffle;
                }
                db.SaveChanges();

                // Set is Dirty for TV
                var tvs = (from t in db.DS_TVContents where t.content_id == contentOptionInfo.content_id select t).ToList();
                foreach (var tv in tvs)
                    tv.DS_TVs.isDirty = 1;

                db.SaveChanges();

                return new ResultMessage<string>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.success,
                        message = "عملیات با موفقیت انجام شد"
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

        public ResultMessage<string> editContentOptionLive(ContentOptionInfoWTO contentOptionInfo)
        {
            try
            {
                DigitalSignageEntities db = new DigitalSignageEntities();
                // Delete all othe video contents
                var contentAds = (from c in db.DS_ContentAds where c.content_id == contentOptionInfo.content_id && c.position == (short)contentOptionInfo.position select c).ToList();
                foreach (var content in contentAds)
                {
                    db.DS_TVContentAds.RemoveRange(content.DS_TVContentAds);
                    db.DS_ContentAds.Remove(content);
                    db.SaveChanges();
                }

                DS_ContentAds contentAd = new DS_ContentAds();
                contentAd.live_id = contentOptionInfo.live_id;
                contentAd.position = (short)contentOptionInfo.position;
                contentAd.content_id = contentOptionInfo.content_id;
                contentAd.secondViewed = 0;
                db.DS_ContentAds.Add(contentAd);
                db.SaveChanges();

                // Set is Dirty for TV
                var tvs = (from t in db.DS_TVContents where t.content_id == contentOptionInfo.content_id select t).ToList();
                foreach (var tv in tvs)
                    tv.DS_TVs.isDirty = 1;

                db.SaveChanges();

                return new ResultMessage<string>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.success,
                        message = "عملیات با موفقیت انجام شد"
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
        private bool editContentAdsItem(AdsInfoWTO adsInfo, int position, long content_id, int interval, int type)
        {
            try
            {
                DigitalSignageEntities db = new DigitalSignageEntities();

                
                //Save
                saveContntAds(adsInfo, position, content_id, interval, type);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ResultMessage<string> saveContentAds(ContentOptionInfoWTO contentOptionInfo)
        {
            try
            {
                DigitalSignageEntities db = new DigitalSignageEntities();

                // delete Content position
                var contentAds = (from c in db.DS_ContentAds where c.content_id == contentOptionInfo.content_id && c.position == (short)contentOptionInfo.position select c).ToList();
                foreach (var c in contentAds)
                {
                    c.modifyDate = DateUtils.getCurrentEnglishDateTimeAsNumber();
                    db.DS_TVContentAds.RemoveRange(c.DS_TVContentAds);
                    db.DS_ContentAds.Remove(c);
                    db.SaveChanges();
                }


                foreach (var item in contentOptionInfo.adsItemList)
                {
                    editContentAdsItem(item, contentOptionInfo.position, contentOptionInfo.content_id, contentOptionInfo.interval, contentOptionInfo.type);
                }

                //var contents = (from c in db.DS_ContentAds where c.content_id == contentOptionInfo.content_id && c.position == (short)contentOptionInfo.position select c).ToList();
                //foreach (var c in contents)
                //{
                //    c.interval = contentOptionInfo.second;
                //    c.shuffle = (byte)contentOptionInfo.shuffle;
                //}
                //db.SaveChanges();


                // Set is Dirty for TV
                var tvs = (from t in db.DS_TVContents where t.content_id == contentOptionInfo.content_id select t).ToList();
                foreach (var tv in tvs)
                    tv.DS_TVs.isDirty = 1;

                db.SaveChanges();

                return new ResultMessage<string>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.success,
                        message = "عملیات با موفقیت انجام شد"
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

        #region Delete
        public ResultMessage<string> deleteContents(long id)
        {
            try
            {
                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_Contents content = (from c in db.DS_Contents where c.id == id select c).FirstOrDefault();
                if (content != null)
                {
                    db.DS_Contents.Remove(content);
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

        public ResultMessage<string> deleteContentsAsdWithPosition(long contentId, int position)
        {
            try
            {
                DigitalSignageEntities db = new DigitalSignageEntities();
                var contentAds = (from i in db.DS_ContentAds where i.content_id == contentId && i.position == position select i).ToList();
                foreach (var item in contentAds)
                {
                    db.DS_ContentAds.Remove(item);
                    db.SaveChanges();
                }

                // Set is Dirty for TV
                var tvs = (from t in db.DS_TVContents where t.content_id == contentId select t).ToList();
                foreach (var tv in tvs)
                    tv.DS_TVs.isDirty = 1;

                db.SaveChanges();

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
        //Generic
        public ResultMessage<List<ContentTypeWTO>> searchDataContentType(ContentTypeWTO filter, PagingInfo paging)
        {
            try
            {
                GetDataManager<ContentTypeWTO, DS_ContentTypes> entitySearch = new GetDataManager<ContentTypeWTO, DS_ContentTypes>();
                List<DS_ContentTypes> lst = entitySearch.getGeneralSearch(filter, paging, "DS_ContentTypes");
                List<ContentTypeWTO> listSearch = entitySearch.MapFrom(lst);
                return new ResultMessage<List<ContentTypeWTO>>
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
                return new ResultMessage<List<ContentTypeWTO>>
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
        public ResultMessage<List<ContentInfoWTO>> searchDataContent(ContentInfoWTO filter, PagingInfo paging)
        {
            try
            {
                GetDataManager<ContentInfoWTO, DS_Contents> entitySearch = new GetDataManager<ContentInfoWTO, DS_Contents>();
                List<DS_Contents> lst = entitySearch.getGeneralSearch(filter, paging, "DS_Contents");
                List<ContentInfoWTO> listSearch = entitySearch.MapFrom(lst);
                return new ResultMessage<List<ContentInfoWTO>>
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
                return new ResultMessage<List<ContentInfoWTO>>
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

        public ResultMessage<List<ContentInfoWTO>> getContentList(ContentInfoWTO filter, PagingInfo paging)
        {
            try
            {
                List<ContentInfoWTO> listSearch = new List<ContentInfoWTO>();
                DigitalSignageEntities db = new DigitalSignageEntities();
                GetDataManager<ContentInfoWTO, DS_Contents> entitySearch = new GetDataManager<ContentInfoWTO, DS_Contents>();
                filter.status = 1;
                List<DS_Contents> contentlist = entitySearch.getGeneralSearch(filter, paging, "DS_Contents");
                foreach (var item in contentlist)
                {
                    ContentInfoWTO newItem = new ContentInfoWTO();
                    newItem.title = item.title;
                    newItem.description = item.description;
                    newItem.id = (long)item.id;
                    newItem.content_id = (long)item.DS_ContentTypes.id;
                    newItem.file_name = item.DS_ContentTypes.file_name;

                    var adDetail = (from i in db.DS_Ads
                                    join j in db.DS_ContentAds on i.id equals j.ad_id
                                    where j.content_id == item.id
                                    select new { i.title }).FirstOrDefault();
                    if (adDetail != null)
                        newItem.ad_title = adDetail.title;
                    listSearch.Add(newItem);
                }

                return new ResultMessage<List<ContentInfoWTO>>
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
                return new ResultMessage<List<ContentInfoWTO>>
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
        public ResultMessage<List<AdsInfoWTO>> searchContentsWithAdsItemDetail(string type, long content_id, int position)
        {
            try
            {
                List<AdsInfoWTO> listSearch = new List<AdsInfoWTO>();
                DigitalSignageEntities db = new DigitalSignageEntities();
                short typeAds = Convert.ToInt16(type);
                var additemLis = (from i in db.DS_Ads
                                  join j in db.DS_ContentAds on i.id equals j.ad_id
                                  where i.type == typeAds && j.content_id == content_id && j.position == position && j.live_id == null
                                  select new { i, j.shuffle, j.position, j.interval }).ToList();
               
                foreach (var item in additemLis)
                {
                    AdsInfoWTO newItem = new AdsInfoWTO();
                    newItem.title = item.i.title;
                    newItem.max_minutes = (int)item.i.max_minutes;
                    newItem.id = (long)item.i.id;
                    newItem.type = (int)item.i.type;
                    newItem.position = (int)item.position;
                    newItem.shuffle = (int)item.shuffle;
                    newItem.companyId = (long)item.i.companyId;

                    newItem.interval = (int)item.interval;
                    newItem.itemList = new List<AdsIemInfoWTO>();
                    var adsDetail = (from j in db.DS_AdItems where j.ad_id == item.i.id select j).ToList();
                    foreach (var detail in adsDetail)
                    {
                        AdsIemInfoWTO newAdsDetail = new AdsIemInfoWTO();
                        newAdsDetail.title = detail.title;
                        newAdsDetail.file_name = detail.file_name;
                        newAdsDetail.id = (long)detail.id;
                        newAdsDetail.description = detail.description;
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

        public ResultMessage<List<AdsInfoWTO>> loadContentsWithAdsItemDetail(long content_id)
        {
            try
            {
                List<AdsInfoWTO> listSearch = new List<AdsInfoWTO>();
                DigitalSignageEntities db = new DigitalSignageEntities();

                //get ads
                var adsitemLis = (from i in db.DS_Ads
                                  join j in db.DS_ContentAds on i.id equals j.ad_id
                                  where  j.content_id == content_id && j.live_id == null
                                  //  && i.passed_minutes < i.max_minutes
                                  select new { i, j.shuffle, j.interval, content_ad_id = j.id, j.position }).ToList();
                foreach (var item in adsitemLis)
                {
                    AdsInfoWTO newItem = new AdsInfoWTO();
                    newItem.title = item.i.title;
                    newItem.max_minutes = (int)item.i.max_minutes;
                    newItem.id = (long)item.i.id;
                    newItem.type = (int)item.i.type;
                    newItem.content_ad_id = (long)item.content_ad_id;
                    newItem.position = (int)item.position;
                    newItem.companyId = (long)item.i.companyId;
                    newItem.interval = (int)item.interval;
                    newItem.itemList = new List<AdsIemInfoWTO>();
                    var adsDetail = (from j in db.DS_AdItems where j.ad_id == item.i.id select j).ToList();
                    foreach (var detail in adsDetail)
                    {
                        AdsIemInfoWTO newAdsDetail = new AdsIemInfoWTO();
                        newAdsDetail.title = detail.title;
                        newAdsDetail.file_name = detail.file_name;
                        newAdsDetail.id = (long)detail.id;
                        newAdsDetail.description = detail.description;
                        newItem.itemList.Add(newAdsDetail);
                    }
                    listSearch.Add(newItem);
                }

                //get Lives
                var livesItemLis = (from i in db.DS_Lives
                                    join j in db.DS_ContentAds on i.id equals j.live_id
                                    where j.content_id == content_id && j.ad_id == null
                                    select new { i, j.shuffle, j.interval, content_ad_id = j.id, j.position }).ToList();
                foreach (var item in livesItemLis)
                {
                    AdsInfoWTO newItem = new AdsInfoWTO();
                    newItem.title = item.i.title;
                    newItem.url = item.i.url;
                    newItem.nameId = (int)item.i.nameId;
                    newItem.id = (long)item.i.id;
                    newItem.type = 6;
                    newItem.content_ad_id = (long)item.content_ad_id;
                    newItem.position = (int)item.position;
                    newItem.companyId = (long)item.i.companyId;
                    newItem.interval = (int)item.interval;
                    newItem.itemList = new List<AdsIemInfoWTO>();

                    AdsIemInfoWTO newAdsDetail = new AdsIemInfoWTO();
                    newAdsDetail.title = item.i.title;
                    newAdsDetail.file_name = item.i.url;
                    newAdsDetail.id = newItem.id;
                    newAdsDetail.description = item.i.description;
                    newItem.itemList.Add(newAdsDetail);

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

        public ResultMessage<List<AdsInfoWTO>> loadContentsWithAdsItemDetail_Viewer(long content_id,long lastAlive)
        {
            try
            {
                List<AdsInfoWTO> listSearch = new List<AdsInfoWTO>();
                DigitalSignageEntities db = new DigitalSignageEntities();

                //get ads
                var adsitemLis = (from i in db.DS_Ads
                                  join j in db.DS_ContentAds on i.id equals j.ad_id
                                  where  j.content_id == content_id && j.live_id == null
                                //  && i.passed_minutes < i.max_minutes
                                  select new { i, j.shuffle, j.interval, content_ad_id = j.id, j.position,j.modifyDate }).ToList();
                foreach (var item in adsitemLis)
                {
                    AdsInfoWTO newItem = new AdsInfoWTO();
                    newItem.title = item.i.title;
                    newItem.max_minutes = (int)item.i.max_minutes;
                    newItem.id = (long)item.i.id;
                    newItem.type = (int)item.i.type;
                    newItem.content_ad_id = (long)item.content_ad_id;
                    newItem.position = (int)item.position;
                    newItem.companyId = (long)item.i.companyId;
                    newItem.interval = (int)item.interval;
                    newItem.itemList = new List<AdsIemInfoWTO>();

                    if (item.modifyDate > lastAlive)
                        newItem.status = 1;//Change
                    else
                        newItem.status = 0;
                    var adsDetail = (from j in db.DS_AdItems where j.ad_id == item.i.id select j).ToList();
                    foreach (var detail in adsDetail)
                    {
                        AdsIemInfoWTO newAdsDetail = new AdsIemInfoWTO();
                        newAdsDetail.title = detail.title;
                        newAdsDetail.file_name = detail.file_name;
                        newAdsDetail.id = (long)detail.id;
                        newAdsDetail.description = detail.description;
                        newItem.itemList.Add(newAdsDetail);
                    }
                    listSearch.Add(newItem);
                }

                //get Lives
                var livesItemLis = (from i in db.DS_Lives
                                    join j in db.DS_ContentAds on i.id equals j.live_id
                                    where j.content_id == content_id && j.ad_id == null
                                    select new { i, j.shuffle, j.interval, content_ad_id = j.id, j.position }).ToList();
                foreach (var item in livesItemLis)
                {
                    AdsInfoWTO newItem = new AdsInfoWTO();
                    newItem.title = item.i.title;
                    newItem.url = item.i.url;
                    newItem.nameId = (int)item.i.nameId;
                    newItem.id = (long)item.i.id;
                    newItem.type = 6;
                    newItem.content_ad_id = (long)item.content_ad_id;
                    newItem.position = (int)item.position;
                    newItem.companyId = (long)item.i.companyId;
                    newItem.interval = (int)item.interval;
                    newItem.itemList = new List<AdsIemInfoWTO>();

                    AdsIemInfoWTO newAdsDetail = new AdsIemInfoWTO();
                    newAdsDetail.title = item.i.title;
                    newAdsDetail.file_name = item.i.url;
                    newAdsDetail.id = newItem.id;
                    newAdsDetail.description = item.i.description;
                    newItem.itemList.Add(newAdsDetail);

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

        public ResultMessage<ContentInfoWTO> searchContentId(string ip, string companyName)
        {
            try
            {
                DigitalSignageEntities db = new DigitalSignageEntities();
                ContentInfoWTO contentInfo = new ContentInfoWTO();
                var tvId = (from t in db.DS_TVs
                            join c in db.DS_Company on t.companyId equals c.id
                            where c.companyName == companyName && t.ip == ip
                            select t.id).FirstOrDefault();
                if (tvId != 0)
                {
                    var contentItemInfo = (from c in db.DS_Contents
                                           join ca in db.DS_TVContents on c.id equals ca.content_id
                                           where ca.tv_id == tvId
                                           select new { c.id, c.type }).FirstOrDefault();

                    if (contentItemInfo != null)
                    {
                        contentInfo.type = Convert.ToInt32(contentItemInfo.type);
                        contentInfo.id = Convert.ToInt64(contentItemInfo.id);
                    }
                }

                return new ResultMessage<ContentInfoWTO>
                {
                    resultSet = contentInfo,
                    result = new Result()
                    {
                        status = Result.state.success,
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResultMessage<ContentInfoWTO>
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

        private DS_ContentAds saveContntAds(AdsInfoWTO adsDetail, int position, long content_id, int interval, int type)
        {
            DigitalSignageEntities db = new DigitalSignageEntities();
            DS_ContentAds content;
            try
            {
                content = new DS_ContentAds();
                content.content_id = content_id;
                if (type == 0)
                    content.ad_id = adsDetail.id;
                else
                    content.live_id = adsDetail.id;
                // content.interval = adsDetail.interval;
                content.interval = adsDetail.interval = interval;
                content.shuffle = (byte)adsDetail.shuffle;
                content.position = (short)position;
                content.companyId = adsDetail.companyId;
                db.DS_ContentAds.Add(content);
                db.SaveChanges();
                return content;
            }
            catch
            {
                return null;
            }
        }
    }
}
