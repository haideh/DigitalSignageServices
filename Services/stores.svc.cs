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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "stores" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select stores.svc or stores.svc.cs at the Solution Explorer and start debugging.
    public class stores : Istores
    {
        private string UrlDecode(string s)
        {
            return HttpUtility.UrlDecode(s, System.Text.Encoding.GetEncoding("ISO-8859-1"));
        }

        #region Add
        public ResultMessage<string> addStore(StoresInfoWTO stores)
        {
            try
            {
                stores.title = UrlDecode(stores.title);
                stores.description = UrlDecode(stores.description);
                stores.phone1 = UrlDecode(stores.phone1);
                stores.phone2 = UrlDecode(stores.phone2);
                stores.image = UrlDecode(stores.image);
                stores.startWorkTime = UrlDecode(stores.startWorkTime);
                stores.endWorkTime = UrlDecode(stores.endWorkTime);
                stores.workingDay = UrlDecode(stores.workingDay);

                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_Stores store = new DS_Stores();
                store.title = stores.title;
                store.description = stores.description;
                store.map_id = stores.map_id;
                store.unit = stores.unit;
                store.x = stores.x;
                store.y = stores.y;
                store.phone1 = stores.phone1;
                store.phone2 = stores.phone2;
                store.startWorkTime = stores.startWorkTime;
                store.endWorkTime = stores.endWorkTime;
                store.workingDay = stores.workingDay;
                store.category_id = stores.category_id;
                store.companyId = stores.companyId;

                if (System.IO.File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + "/modules/DigitalSignage/data/uploader/" + stores.image))
                {
                    System.IO.File.Copy(HttpContext.Current.Request.PhysicalApplicationPath + "/modules/DigitalSignage/data/uploader/" + stores.image, HttpContext.Current.Request.PhysicalApplicationPath + "/modules/DigitalSignage/data/stores/" + stores.image);
                    try
                    {
                        System.IO.File.Delete(HttpContext.Current.Request.PhysicalApplicationPath + "/modules/DigitalSignage/data/uploader/" + stores.image);
                    }
                    catch (Exception)
                    {
                    }
                    store.image = stores.image;
                }

                db.DS_Stores.Add(store);
                db.SaveChanges();

                return new ResultMessage<string>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.success,
                        message = "فروشگاه با موفقیت ثبت شد",
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
        public ResultMessage<string> editStore(StoresInfoWTO stores)
        {
            try
            {
                stores.title = UrlDecode(stores.title);
                stores.description = UrlDecode(stores.description);
                stores.phone1 = UrlDecode(stores.phone1);
                stores.phone2 = UrlDecode(stores.phone2);
                stores.image = UrlDecode(stores.image);
                stores.startWorkTime = UrlDecode(stores.startWorkTime);
                stores.endWorkTime = UrlDecode(stores.endWorkTime);
                stores.workingDay = UrlDecode(stores.workingDay);

                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_Stores store = (from p in db.DS_Stores where p.id == stores.id select p).FirstOrDefault();
                if (store != null)
                {
                    store.title = stores.title;
                    store.description = stores.description;
                    store.map_id = stores.map_id;
                    store.unit = stores.unit;
                    store.x = stores.x;
                    store.y = stores.y;
                    store.phone1 = stores.phone1;
                    store.phone2 = stores.phone2;
                    store.startWorkTime = stores.startWorkTime;
                    store.endWorkTime = stores.endWorkTime;
                    store.workingDay = stores.workingDay;
                    store.category_id = stores.category_id;
                    store.companyId = stores.companyId;

                    if (System.IO.File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + "/modules/DigitalSignage/data/uploader/" + stores.image))
                    {
                        System.IO.File.Copy(HttpContext.Current.Request.PhysicalApplicationPath + "/modules/DigitalSignage/data/uploader/" + stores.image, HttpContext.Current.Request.PhysicalApplicationPath + "/modules/DigitalSignage/data/phonebook/" + stores.image);
                        try
                        {
                            System.IO.File.Delete(HttpContext.Current.Request.PhysicalApplicationPath + "/modules/DigitalSignage/data/uploader/" + stores.image);
                        }
                        catch (Exception)
                        {
                        }
                        store.image = stores.image;
                    }

                    db.SaveChanges();
                }

                return new ResultMessage<string>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.success,
                        message = "فروشگاه با موفقیت ویرایش شد",
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
        public ResultMessage<string> deleteStore(long id)
        {
            try
            {

                DigitalSignageEntities db = new DigitalSignageEntities();
                DS_Stores store = (from p in db.DS_Stores where p.id == id select p).FirstOrDefault();
                if (store != null)
                {
                    db.DS_Stores.Remove(store);
                    db.SaveChanges();
                }

                return new ResultMessage<string>
                {
                    resultSet = null,
                    result = new Result()
                    {
                        status = Result.state.success,
                        message = "فروشگاه با موفقیت حذف شد",

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
                        message = ex.Message,
                    }
                };
            }
        }
        #endregion

        #region Search
        public ResultMessage<List<StoresInfoWTO>> searchDataOnStores(StoresInfoWTO filter, PagingInfo paging)
        {
            try
            {
                GetDataManager<StoresInfoWTO, DS_StoreCategories> entitySearch = new GetDataManager<StoresInfoWTO, DS_StoreCategories>();
                List<DS_StoreCategories> lst = entitySearch.getGeneralSearch(filter, paging, "DS_StoreCategories");
                List<StoresInfoWTO> listSearch = entitySearch.MapFrom(lst);
                return new ResultMessage<List<StoresInfoWTO>>
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
                return new ResultMessage<List<StoresInfoWTO>>
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
