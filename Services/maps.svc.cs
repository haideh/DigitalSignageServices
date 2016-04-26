using Aryaban.Engine.Core.WebService;
using DigitalServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DigitalServices.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "maps" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select maps.svc or maps.svc.cs at the Solution Explorer and start debugging.
    public class maps : Imaps
    {
        public ResultMessage<List<MapsInfoWTO>> searchDataOnMaps(MapsInfoWTO filter, PagingInfo paging)
        {
            try
            {
                GetDataManager<MapsInfoWTO, DS_Maps> entitySearch = new GetDataManager<MapsInfoWTO, DS_Maps>();
                List<DS_Maps> lst = entitySearch.getGeneralSearch(filter, paging, "DS_Maps");
                List<MapsInfoWTO> listSearch = entitySearch.MapFrom(lst);
                return new ResultMessage<List<MapsInfoWTO>>
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
                return new ResultMessage<List<MapsInfoWTO>>
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
    }
}
