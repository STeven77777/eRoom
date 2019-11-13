using eRoom.CoreLib.Common;
using eRoom.Shared.CoreLib.Models.Request;
using eRoom.Shared.CoreLib.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRoom.CoreLib.DAL
{
    public interface IMotorTypesDAL
    {
        Task<(DefaultMetaResult h, MotorTypeInfoResponse r)> GetMotorTypeInfo(MotorTypeInfoRequest _param);
        Task<(DefaultMetaResult h, PagingResult<MotorTypeInfoResponse> r)> GetMotorTypeList(MotorTypeListRequest _param);
        Task<(DefaultMetaResult, MotorTypeAddResponse)> InsertMotorType(MotorTypeAddRequest motorTypeAddRequest);
        Task<(DefaultMetaResult, MotorTypeUpdateResponse)> UpdateMotorType(MotorTypeUpdateRequest motorTypeAddRequest);
    }
    public class MotorTypesDAL : BaseDAL, IMotorTypesDAL
    {
        public MotorTypesDAL(IConfiguration _configuration, ILogger<MotorTypesDAL> logger) : base(_configuration, logger)
        {
        }
    
        public async Task<(DefaultMetaResult h, MotorTypeInfoResponse r)> GetMotorTypeInfo(MotorTypeInfoRequest _param)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, MotorTypeInfoResponse>(SP.GetMotorTypeInfo, _param);
        }

        public async Task<(DefaultMetaResult h, PagingResult<MotorTypeInfoResponse> r)> GetMotorTypeList(MotorTypeListRequest _param)
        {
            return await ExecSPReturnListWithAsync<DefaultMetaResult, MotorTypeInfoResponse>(SP.GetMotorTypeList, _param);
        }

        public async Task<(DefaultMetaResult, MotorTypeAddResponse)> InsertMotorType(MotorTypeAddRequest motorTypeAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, MotorTypeAddResponse>(SP.InsertMotorType, motorTypeAddRequest);
        }

        public async Task<(DefaultMetaResult, MotorTypeUpdateResponse)> UpdateMotorType(MotorTypeUpdateRequest motorTypeAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, MotorTypeUpdateResponse>(SP.UpdateMotorType, motorTypeAddRequest);
        }
    }
}
