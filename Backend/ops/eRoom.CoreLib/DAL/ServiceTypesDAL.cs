using eRoom.CoreLib.Common;
using eRoom.Shared.CoreLib.Models.Request;
using eRoom.Shared.CoreLib.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRoom.CoreLib.DAL
{
    public interface IServiceTypesDAL
    {
        Task<(DefaultMetaResult h, ServiceTypeInfoResponse r)> GetServiceTypeInfo(ServiceTypeInfoRequest _param);
        Task<(DefaultMetaResult h, PagingResult<ServiceTypeInfoResponse> r)> GetServiceTypeList(ServiceTypeListRequest _param);
        Task<(DefaultMetaResult, ServiceTypeAddResponse)> InsertServiceType(ServiceTypeAddRequest serviceTypeAddRequest);
        Task<(DefaultMetaResult, ServiceTypeUpdateResponse)> UpdateServiceType(ServiceTypeUpdateRequest serviceTypeAddRequest);
    }
    public class ServiceTypesDAL : BaseDAL, IServiceTypesDAL
    {
        public ServiceTypesDAL(IConfiguration _configuration, ILogger<ServiceTypesDAL> logger) : base(_configuration, logger)
        {
        }
    
        public async Task<(DefaultMetaResult h, ServiceTypeInfoResponse r)> GetServiceTypeInfo(ServiceTypeInfoRequest _param)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, ServiceTypeInfoResponse>(SP.GetServiceTypeInfo, _param);
        }

        public async Task<(DefaultMetaResult h, PagingResult<ServiceTypeInfoResponse> r)> GetServiceTypeList(ServiceTypeListRequest _param)
        {
            return await ExecSPReturnListWithAsync<DefaultMetaResult, ServiceTypeInfoResponse>(SP.GetServiceTypeList, _param);
        }

        public async Task<(DefaultMetaResult, ServiceTypeAddResponse)> InsertServiceType(ServiceTypeAddRequest serviceTypeAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, ServiceTypeAddResponse>(SP.InsertServiceType, serviceTypeAddRequest);
        }

        public async Task<(DefaultMetaResult, ServiceTypeUpdateResponse)> UpdateServiceType(ServiceTypeUpdateRequest serviceTypeAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, ServiceTypeUpdateResponse>(SP.UpdateServiceType, serviceTypeAddRequest);
        }
    }
}
