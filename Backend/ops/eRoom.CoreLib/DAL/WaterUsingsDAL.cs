using eRoom.CoreLib.Common;
using eRoom.Shared.CoreLib.Models.Request;
using eRoom.Shared.CoreLib.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRoom.CoreLib.DAL
{
    public interface IWaterUsingsDAL
    {
        Task<(DefaultMetaResult h, WaterUsingInfoResponse r)> GetWaterUsingInfo(WaterUsingInfoRequest _param);
        Task<(DefaultMetaResult h, PagingResult<WaterUsingInfoResponse> r)> GetWaterUsingList(WaterUsingListRequest _param);
        Task<(DefaultMetaResult, WaterUsingAddResponse)> InsertWaterUsing(WaterUsingAddRequest roomAddRequest);
        Task<(DefaultMetaResult, WaterUsingUpdateResponse)> UpdateWaterUsing(WaterUsingUpdateRequest roomAddRequest);
    }
    public class WaterUsingsDAL : BaseDAL, IWaterUsingsDAL
    {
        public WaterUsingsDAL(IConfiguration _configuration, ILogger<WaterUsingsDAL> logger) : base(_configuration, logger)
        {
        }
    
        public async Task<(DefaultMetaResult h, WaterUsingInfoResponse r)> GetWaterUsingInfo(WaterUsingInfoRequest _param)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, WaterUsingInfoResponse>(SP.GetWaterUsingInfo, _param);
        }

        public async Task<(DefaultMetaResult h, PagingResult<WaterUsingInfoResponse> r)> GetWaterUsingList(WaterUsingListRequest _param)
        {
            return await ExecSPReturnListWithAsync<DefaultMetaResult, WaterUsingInfoResponse>(SP.GetWaterUsingList, _param);
        }

        public async Task<(DefaultMetaResult, WaterUsingAddResponse)> InsertWaterUsing(WaterUsingAddRequest roomAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, WaterUsingAddResponse>(SP.InsertWaterUsing, roomAddRequest);
        }

        public async Task<(DefaultMetaResult, WaterUsingUpdateResponse)> UpdateWaterUsing(WaterUsingUpdateRequest roomAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, WaterUsingUpdateResponse>(SP.UpdateWaterUsing, roomAddRequest);
        }
    }
}
