using eRoom.CoreLib.Common;
using eRoom.Shared.CoreLib.Models.Request;
using eRoom.Shared.CoreLib.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRoom.CoreLib.DAL
{
    public interface IRoomTypesDAL
    {
        Task<(DefaultMetaResult h, RoomTypeInfoResponse r)> GetRoomTypeInfo(RoomTypeInfoRequest _param);
        Task<(DefaultMetaResult h, PagingResult<RoomTypeInfoResponse> r)> GetRoomTypeList(RoomTypeListRequest _param);
        Task<(DefaultMetaResult, RoomTypeAddResponse)> InsertRoomType(RoomTypeAddRequest roomTypeAddRequest);
        Task<(DefaultMetaResult, RoomTypeUpdateResponse)> UpdateRoomType(RoomTypeUpdateRequest roomTypeAddRequest);
    }
    public class RoomTypesDAL : BaseDAL, IRoomTypesDAL
    {
        public RoomTypesDAL(IConfiguration _configuration, ILogger<RoomTypesDAL> logger) : base(_configuration, logger)
        {
        }
    
        public async Task<(DefaultMetaResult h, RoomTypeInfoResponse r)> GetRoomTypeInfo(RoomTypeInfoRequest _param)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, RoomTypeInfoResponse>(SP.GetRoomTypeInfo, _param);
        }

        public async Task<(DefaultMetaResult h, PagingResult<RoomTypeInfoResponse> r)> GetRoomTypeList(RoomTypeListRequest _param)
        {
            return await ExecSPReturnListWithAsync<DefaultMetaResult, RoomTypeInfoResponse>(SP.GetRoomTypeList, _param);
        }

        public async Task<(DefaultMetaResult, RoomTypeAddResponse)> InsertRoomType(RoomTypeAddRequest roomTypeAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, RoomTypeAddResponse>(SP.InsertRoomType, roomTypeAddRequest);
        }

        public async Task<(DefaultMetaResult, RoomTypeUpdateResponse)> UpdateRoomType(RoomTypeUpdateRequest roomTypeAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, RoomTypeUpdateResponse>(SP.UpdateRoomType, roomTypeAddRequest);
        }
    }
}
