using eRoom.CoreLib.Common;
using eRoom.Shared.CoreLib.Models.Request;
using eRoom.Shared.CoreLib.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRoom.CoreLib.DAL
{
    public interface IServicesDAL
    {
        Task<(DefaultMetaResult h, ServiceInfoResponse r)> GetServiceInfo(ServiceInfoRequest _param);
        Task<(DefaultMetaResult h, PagingResult<ServiceInfoResponse> r)> GetServiceList(ServiceListRequest _param);
        Task<(DefaultMetaResult, ServiceAddResponse)> InsertService(ServiceAddRequest serviceAddRequest);
        Task<(DefaultMetaResult, ServiceUpdateResponse)> UpdateService(ServiceUpdateRequest serviceAddRequest);
    }
    public class ServicesDAL : BaseDAL, IServicesDAL
    {
        public ServicesDAL(IConfiguration _configuration, ILogger<ServicesDAL> logger) : base(_configuration, logger)
        {
        }
    
        public async Task<(DefaultMetaResult h, ServiceInfoResponse r)> GetServiceInfo(ServiceInfoRequest _param)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, ServiceInfoResponse>(SP.GetServiceInfo, _param);
        }

        public async Task<(DefaultMetaResult h, PagingResult<ServiceInfoResponse> r)> GetServiceList(ServiceListRequest _param)
        {
            return await ExecSPReturnListWithAsync<DefaultMetaResult, ServiceInfoResponse>(SP.GetServiceList, _param);
        }

        public async Task<(DefaultMetaResult, ServiceAddResponse)> InsertService(ServiceAddRequest serviceAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, ServiceAddResponse>(SP.InsertService, serviceAddRequest);
        }

        public async Task<(DefaultMetaResult, ServiceUpdateResponse)> UpdateService(ServiceUpdateRequest serviceAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, ServiceUpdateResponse>(SP.UpdateService, serviceAddRequest);
        }
    }
}
