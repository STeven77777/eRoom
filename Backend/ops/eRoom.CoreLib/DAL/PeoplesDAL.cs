using eRoom.CoreLib.Common;
using eRoom.Shared.CoreLib.Models.Request;
using eRoom.Shared.CoreLib.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRoom.CoreLib.DAL
{
    public interface IPeoplesDAL
    {
        Task<(DefaultMetaResult h, PeopleInfoResponse r)> GetPeopleInfo(PeopleInfoRequest _param);
        Task<(DefaultMetaResult h, PagingResult<PeopleInfoResponse> r)> GetPeopleList(PeopleListRequest _param);
        Task<(DefaultMetaResult, PeopleAddResponse)> InsertPeople(PeopleAddRequest roomAddRequest);
        Task<(DefaultMetaResult, PeopleUpdateResponse)> UpdatePeople(PeopleUpdateRequest roomAddRequest);
    }
    public class PeoplesDAL : BaseDAL, IPeoplesDAL
    {
        public PeoplesDAL(IConfiguration _configuration, ILogger<PeoplesDAL> logger) : base(_configuration, logger)
        {
        }
    
        public async Task<(DefaultMetaResult h, PeopleInfoResponse r)> GetPeopleInfo(PeopleInfoRequest _param)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, PeopleInfoResponse>(SP.GetPeopleInfo, _param);
        }

        public async Task<(DefaultMetaResult h, PagingResult<PeopleInfoResponse> r)> GetPeopleList(PeopleListRequest _param)
        {
            return await ExecSPReturnListWithAsync<DefaultMetaResult, PeopleInfoResponse>(SP.GetPeopleList, _param);
        }

        public async Task<(DefaultMetaResult, PeopleAddResponse)> InsertPeople(PeopleAddRequest roomAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, PeopleAddResponse>(SP.InsertPeople, roomAddRequest);
        }

        public async Task<(DefaultMetaResult, PeopleUpdateResponse)> UpdatePeople(PeopleUpdateRequest roomAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, PeopleUpdateResponse>(SP.UpdatePeople, roomAddRequest);
        }
    }
}
