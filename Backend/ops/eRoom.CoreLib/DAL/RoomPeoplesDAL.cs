using eRoom.CoreLib.Common;
using eRoom.Shared.CoreLib.Models.Request;
using eRoom.Shared.CoreLib.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRoom.CoreLib.DAL
{
    public interface IRoomPeoplesDAL
    {
        Task<(DefaultMetaResult h, RoomPeopleInfoResponse r)> GetRoomPeopleInfo(RoomPeopleInfoRequest _param);
        Task<(DefaultMetaResult h, PagingResult<RoomPeopleInfoResponse> r)> GetRoomPeopleList(RoomPeopleListRequest _param);
        Task<(DefaultMetaResult, RoomPeopleAddResponse)> InsertRoomPeople(RoomPeopleAddRequest roomPeopleAddRequest);
        Task<(DefaultMetaResult, RoomPeopleUpdateResponse)> UpdateRoomPeople(RoomPeopleUpdateRequest roomPeopleAddRequest);
    }
    public class RoomPeoplesDAL : BaseDAL, IRoomPeoplesDAL
    {
        public RoomPeoplesDAL(IConfiguration _configuration, ILogger<RoomPeoplesDAL> logger) : base(_configuration, logger)
        {
        }
    
        public async Task<(DefaultMetaResult h, RoomPeopleInfoResponse r)> GetRoomPeopleInfo(RoomPeopleInfoRequest _param)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, RoomPeopleInfoResponse>(SP.GetRoomPeopleInfo, _param);
        }

        public async Task<(DefaultMetaResult h, PagingResult<RoomPeopleInfoResponse> r)> GetRoomPeopleList(RoomPeopleListRequest _param)
        {
            return await ExecSPReturnListWithAsync<DefaultMetaResult, RoomPeopleInfoResponse>(SP.GetRoomPeopleList, _param);
        }

        public async Task<(DefaultMetaResult, RoomPeopleAddResponse)> InsertRoomPeople(RoomPeopleAddRequest roomPeopleAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, RoomPeopleAddResponse>(SP.InsertRoomPeople, roomPeopleAddRequest);
        }

        public async Task<(DefaultMetaResult, RoomPeopleUpdateResponse)> UpdateRoomPeople(RoomPeopleUpdateRequest roomPeopleAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, RoomPeopleUpdateResponse>(SP.UpdateRoomPeople, roomPeopleAddRequest);
        }
    }
}
