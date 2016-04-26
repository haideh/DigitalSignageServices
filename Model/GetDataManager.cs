using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DigitalServices.Model
{

    public class GetDataManager<EntityT, TableT>
    {
        public Dictionary<FieldInfo, string> getEntityFieldInfo(EntityT t)
        {
            Dictionary<FieldInfo, string> entityFieldInfo = new Dictionary<FieldInfo, string>();
            foreach (var item in t.GetType().GetProperties())
            {
                if (item.GetValue(t) == null)
                    continue;
                else if (item.GetValue(t).ToString() == "0")
                    continue;
                else
                {
                    FieldInfo field = new FieldInfo();
                    field.title = item.Name;
                    field.type = item.PropertyType.ToString();
                    entityFieldInfo.Add(field, item.GetValue(t).ToString());

                }
            }
            return entityFieldInfo;
        }

        public List<TableT> getGeneralSearch(EntityT filter, PagingInfo paging, string tableName)
        {

            DigitalSignageEntities db = new DigitalSignageEntities();
            StringBuilder sb = new StringBuilder();

            if (paging == null)
            {
                PagingInfo defaultPaging = new PagingInfo();
                defaultPaging.orderLst = new List<OrderInfo>();
                OrderInfo o = new OrderInfo();
                defaultPaging.orderLst.Add(o);
                paging = defaultPaging;
            }

            sb.Append("select * from (SELECT ROW_NUMBER() OVER(ORDER BY ");
            foreach (var item in paging.orderLst)
            {
                sb.Append(item.orderName + " " + item.sortType + ", ");
            }
            string tempString = sb.ToString().Substring(0, sb.ToString().Length - 2); ;
            sb.Clear();
            sb.Append(tempString);

            sb.Append(") AS NUMBER, * FROM " + tableName);
            Dictionary<FieldInfo, string> info = getEntityFieldInfo(filter);
            if (info.Count > 0)
                sb.Append(" where ");
            foreach (var item in info)
            {
                if (!string.IsNullOrEmpty(item.Value.ToString()))
                {
                    if (item.Key.type.ToLower().Contains("string"))
                        sb.Append(item.Key.title.ToString() + " like N'%" + item.Value.ToString() + "%' And ");
                    else
                        sb.Append(item.Key.title.ToString() + "=" + item.Value.ToString() + " And ");
                }
            }

            tempString = sb.ToString().Substring(0, sb.ToString().Length - 4);
            sb.Clear();
            sb.Append(tempString);

            sb.Append(" ) AS TBL ");
            sb.Append("WHERE NUMBER BETWEEN((" + paging.pageNumber + "- 1) *" + paging.pageSize + "+ 1) AND(" + paging.pageNumber + "*" + paging.pageSize + ")");

            string query = sb.ToString();
            List<TableT> lst = db.Database.SqlQuery<TableT>(query).ToList<TableT>();
            return lst;
        }

        #region Map

        public List<EntityT> MapFrom(List<TableT> wtos)
        {
            List<EntityT> mapList = new List<EntityT>();

            if (wtos != null)
                foreach (TableT c in wtos)
                {
                    mapList.Add(MapFrom(c));
                }

            return mapList;
        }
        public EntityT MapFrom(TableT color)
        {
            AutoMapper.Mapper.CreateMap<TableT, EntityT>();
            EntityT wto = AutoMapper.Mapper.Map<TableT, EntityT>(color);

            return wto;
        }

        #endregion
    }
}