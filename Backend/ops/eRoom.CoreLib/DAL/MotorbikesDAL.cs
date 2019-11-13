using eRoom.CoreLib.Common;
using eRoom.Shared.CoreLib.Models.Request;
using eRoom.Shared.CoreLib.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRoom.CoreLib.DAL
{
    public interface IMotorbikesDAL
    {
        Task<(DefaultMetaResult h, MotorbikeInfoResponse r)> GetMotorbikeInfo(MotorbikeInfoRequest _param);
        Task<(DefaultMetaResult h, PagingResult<MotorbikeInfoResponse> r)> GetMotorbikeList(MotorbikeListRequest _param);
        Task<(DefaultMetaResult, MotorbikeAddResponse)> InsertMotorbike(MotorbikeAddRequest motorbikeAddRequest);
        Task<(DefaultMetaResult, MotorbikeUpdateResponse)> UpdateMotorbike(MotorbikeUpdateRequest motorbikeAddRequest);
    }
    public class MotorbikesDAL : BaseDAL, IMotorbikesDAL
    {
        public MotorbikesDAL(IConfiguration _configuration, ILogger<MotorbikesDAL> logger) : base(_configuration, logger)
        {
        }
    
        public async Task<(DefaultMetaResult h, MotorbikeInfoResponse r)> GetMotorbikeInfo(MotorbikeInfoRequest _param)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, MotorbikeInfoResponse>(SP.GetMotorbikeInfo, _param);
        }

        public async Task<(DefaultMetaResult h, PagingResult<MotorbikeInfoResponse> r)> GetMotorbikeList(MotorbikeListRequest _param)
        {
            return await ExecSPReturnListWithAsync<DefaultMetaResult, MotorbikeInfoResponse>(SP.GetMotorbikeList, _param);
        }

        public async Task<(DefaultMetaResult, MotorbikeAddResponse)> InsertMotorbike(MotorbikeAddRequest motorbikeAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, MotorbikeAddResponse>(SP.InsertMotorbike, motorbikeAddRequest);
        }

        public async Task<(DefaultMetaResult, MotorbikeUpdateResponse)> UpdateMotorbike(MotorbikeUpdateRequest motorbikeAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, MotorbikeUpdateResponse>(SP.UpdateMotorbike, motorbikeAddRequest);
        }
    }
}
