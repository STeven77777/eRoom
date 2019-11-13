using eRoom.CoreLib.Common;
using eRoom.Shared.CoreLib.Models.Request;
using eRoom.Shared.CoreLib.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRoom.CoreLib.DAL
{
    public interface IRoomStatusDAL
    {
        Task<(DefaultMetaResult h, RoomStatusInfoResponse r)> GetRoomStatusInfo(RoomStatusInfoRequest _param);
        Task<(DefaultMetaResult h, PagingResult<RoomStatusInfoResponse> r)> GetRoomStatusList(RoomStatusListRequest _param);
        Task<(DefaultMetaResult, RoomStatusAddResponse)> InsertRoomStatus(RoomStatusAddRequest roomStatusAddRequest);
        Task<(DefaultMetaResult, RoomStatusUpdateResponse)> UpdateRoomStatus(RoomStatusUpdateRequest roomStatusAddRequest);
    }
    public class RoomStatusDAL : BaseDAL, IRoomStatusDAL
    {
        public RoomStatusDAL(IConfiguration _configuration, ILogger<RoomStatusDAL> logger) : base(_configuration, logger)
        {
        }
    
        public async Task<(DefaultMetaResult h, RoomStatusInfoResponse r)> GetRoomStatusInfo(RoomStatusInfoRequest _param)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, RoomStatusInfoResponse>(SP.GetRoomStatusInfo, _param);
        }

        public async Task<(DefaultMetaResult h, PagingResult<RoomStatusInfoResponse> r)> GetRoomStatusList(RoomStatusListRequest _param)
        {
            return await ExecSPReturnListWithAsync<DefaultMetaResult, RoomStatusInfoResponse>(SP.GetRoomStatusList, _param);
        }

        public async Task<(DefaultMetaResult, RoomStatusAddResponse)> InsertRoomStatus(RoomStatusAddRequest roomStatusAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, RoomStatusAddResponse>(SP.InsertRoomStatus, roomStatusAddRequest);
        }

        public async Task<(DefaultMetaResult, RoomStatusUpdateResponse)> UpdateRoomStatus(RoomStatusUpdateRequest roomStatusAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, RoomStatusUpdateResponse>(SP.UpdateRoomStatus, roomStatusAddRequest);
        }
    }
}
