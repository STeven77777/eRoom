using eRoom.CoreLib.Common;
using eRoom.Shared.CoreLib.Models.Request;
using eRoom.Shared.CoreLib.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRoom.CoreLib.DAL
{
    public interface IRoomDetailsDAL
    {
        Task<(DefaultMetaResult h, RoomDetailInfoResponse r)> GetRoomDetailInfo(RoomDetailInfoRequest _param);
        Task<(DefaultMetaResult h, PagingResult<RoomDetailInfoResponse> r)> GetRoomDetailList(RoomDetailListRequest _param);
        Task<(DefaultMetaResult, RoomDetailAddResponse)> InsertRoomDetail(RoomDetailAddRequest roomDetailAddRequest);
        Task<(DefaultMetaResult, RoomDetailUpdateResponse)> UpdateRoomDetail(RoomDetailUpdateRequest roomDetailAddRequest);
    }
    public class RoomDetailsDAL : BaseDAL, IRoomDetailsDAL
    {
        public RoomDetailsDAL(IConfiguration _configuration, ILogger<RoomDetailsDAL> logger) : base(_configuration, logger)
        {
        }
    
        public async Task<(DefaultMetaResult h, RoomDetailInfoResponse r)> GetRoomDetailInfo(RoomDetailInfoRequest _param)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, RoomDetailInfoResponse>(SP.GetRoomDetailInfo, _param);
        }

        public async Task<(DefaultMetaResult h, PagingResult<RoomDetailInfoResponse> r)> GetRoomDetailList(RoomDetailListRequest _param)
        {
            return await ExecSPReturnListWithAsync<DefaultMetaResult, RoomDetailInfoResponse>(SP.GetRoomDetailList, _param);
        }

        public async Task<(DefaultMetaResult, RoomDetailAddResponse)> InsertRoomDetail(RoomDetailAddRequest roomDetailAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, RoomDetailAddResponse>(SP.InsertRoomDetail, roomDetailAddRequest);
        }

        public async Task<(DefaultMetaResult, RoomDetailUpdateResponse)> UpdateRoomDetail(RoomDetailUpdateRequest roomDetailAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, RoomDetailUpdateResponse>(SP.UpdateRoomDetail, roomDetailAddRequest);
        }
    }
}
