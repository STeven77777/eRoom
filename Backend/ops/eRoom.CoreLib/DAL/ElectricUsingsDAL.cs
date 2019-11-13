using eRoom.CoreLib.Common;
using eRoom.Shared.CoreLib.Models.Request;
using eRoom.Shared.CoreLib.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRoom.CoreLib.DAL
{
    public interface IElectricUsingsDAL
    {
        Task<(DefaultMetaResult h, ElectricUsingInfoResponse r)> GetElectricUsingInfo(ElectricUsingInfoRequest _param);
        Task<(DefaultMetaResult h, PagingResult<ElectricUsingInfoResponse> r)> GetElectricUsingList(ElectricUsingListRequest _param);
        Task<(DefaultMetaResult, ElectricUsingAddResponse)> InsertElectricUsing(ElectricUsingAddRequest electricUsingAddRequest);
        Task<(DefaultMetaResult, ElectricUsingUpdateResponse)> UpdateElectricUsing(ElectricUsingUpdateRequest electricUsingAddRequest);
    }
    public class ElectricUsingsDAL : BaseDAL, IElectricUsingsDAL
    {
        public ElectricUsingsDAL(IConfiguration _configuration, ILogger<ElectricUsingsDAL> logger) : base(_configuration, logger)
        {
        }
    
        public async Task<(DefaultMetaResult h, ElectricUsingInfoResponse r)> GetElectricUsingInfo(ElectricUsingInfoRequest _param)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, ElectricUsingInfoResponse>(SP.GetElectricUsingInfo, _param);
        }

        public async Task<(DefaultMetaResult h, PagingResult<ElectricUsingInfoResponse> r)> GetElectricUsingList(ElectricUsingListRequest _param)
        {
            return await ExecSPReturnListWithAsync<DefaultMetaResult, ElectricUsingInfoResponse>(SP.GetElectricUsingList, _param);
        }

        public async Task<(DefaultMetaResult, ElectricUsingAddResponse)> InsertElectricUsing(ElectricUsingAddRequest electricUsingAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, ElectricUsingAddResponse>(SP.InsertElectricUsing, electricUsingAddRequest);
        }

        public async Task<(DefaultMetaResult, ElectricUsingUpdateResponse)> UpdateElectricUsing(ElectricUsingUpdateRequest electricUsingAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, ElectricUsingUpdateResponse>(SP.UpdateElectricUsing, electricUsingAddRequest);
        }
    }
}
