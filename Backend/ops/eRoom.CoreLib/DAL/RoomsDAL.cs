using eRoom.CoreLib.Common;
using eRoom.Shared.CoreLib.Models.Request;
using eRoom.Shared.CoreLib.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRoom.CoreLib.DAL
{
    public interface IRoomsDAL
    {
        Task<(DefaultMetaResult h, RoomInfoResponse r)> GetRoomInfo(RoomInfoRequest _param);
        Task<(DefaultMetaResult h, PagingResult<RoomInfoResponse> r)> GetRoomList(RoomListRequest _param);
        Task<(DefaultMetaResult, RoomAddResponse)> InsertRoom(RoomAddRequest roomAddRequest);
        Task<(DefaultMetaResult, RoomUpdateResponse)> UpdateRoom(RoomUpdateRequest roomAddRequest);
        Task<(DefaultMetaResult, RoomAddResponse)> BackupRoom();
    }
    public class RoomsDAL : BaseDAL, IRoomsDAL
    {
        public RoomsDAL(IConfiguration _configuration, ILogger<RoomsDAL> logger) : base(_configuration, logger)
        {
        }
    
        public async Task<(DefaultMetaResult h, RoomInfoResponse r)> GetRoomInfo(RoomInfoRequest _param)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, RoomInfoResponse>(SP.GetRoomInfo, _param);
        }

        public async Task<(DefaultMetaResult h, PagingResult<RoomInfoResponse> r)> GetRoomList(RoomListRequest _param)
        {
            return await ExecSPReturnListWithAsync<DefaultMetaResult, RoomInfoResponse>(SP.GetRoomList, _param);
        }

        public async Task<(DefaultMetaResult, RoomAddResponse)> InsertRoom(RoomAddRequest roomAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, RoomAddResponse>(SP.InsertRoom, roomAddRequest);
        }

        public async Task<(DefaultMetaResult, RoomUpdateResponse)> UpdateRoom(RoomUpdateRequest roomAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, RoomUpdateResponse>(SP.UpdateRoom, roomAddRequest);
        }

        public async Task<(DefaultMetaResult, RoomAddResponse)> BackupRoom()
        {
            return await BackupRoomDAL();
        }

       
        private async Task<(DefaultMetaResult, RoomAddResponse)> BackupRoomDAL()
        {
            DefaultMetaResult response = new DefaultMetaResult();
            RoomAddResponse result = new RoomAddResponse();
            try
            {              
                ServerConnection serverConnection = new ServerConnection(@"DEV-TUNGPHAM\SQLEXPRESS", "sa", "pst@2019?a");
                Server server = new Server(serverConnection);
                Database database = server.Databases["eRoom"];
                Backup backup = new Backup();
                backup.Action = BackupActionType.Database;
                backup.BackupSetDescription = "eRoom - full backup";
                backup.BackupSetName = "eRoom backup";
                backup.Database = "eRoom";

                BackupDeviceItem deviceItem = new BackupDeviceItem(@"E:\Logs\db\eRoom_Full_Backup_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak", DeviceType.File);
                backup.Devices.Add(deviceItem);
                backup.Incremental = false;
                backup.LogTruncation = BackupTruncateLogType.Truncate;
                backup.SqlBackup(server);
                result.RoomID = "Success";
            }
            catch (Exception ex)
            {
                result.RoomID = GetErrorResponse(ex);
            }
            return await Task.Factory.StartNew(() => (response, result));

        }

        private static string GetErrorResponse(Exception e)
        {
            string message = e.Message;
            string innerMessage = e.InnerException == null ? "" : e.InnerException.Message;
            string innerInnerMessage = string.Empty;
            if (e.InnerException != null && e.InnerException.InnerException != null && e.InnerException.InnerException != null)
            {
                innerInnerMessage = e.InnerException.InnerException.Message;
            }
            string stackTrace = e.StackTrace == null ? "" : e.StackTrace.ToString();
            return string.Format("{0} {1} {2} {3}", message, innerMessage, innerInnerMessage, stackTrace);
        }

      
    }
}
