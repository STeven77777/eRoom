using AutoMapper;
using eRoom.CoreLib.DAL;
using eRoom.Shared.Api.Infrastructure.Utils;
using eRoom.Shared.CoreLib.Models.Request;
using eRoom.Shared.CoreLib.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace eRoom.API.Controllers
{
    [Route("api/rooms")]
    [Produces("application/json")]
    [ApiController]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 400)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 404)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 500)]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomsDAL roomsDAL;
        private readonly IMapper mapper;
        private ILogger logger;
        private string ModuleName = "eRoom.API";//System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public RoomsController(IRoomsDAL _roomsDAL, ILogger<RoomsController> _logger, IMapper _mapper)
        {
            roomsDAL = _roomsDAL;
            logger = _logger;
            mapper = _mapper;
        }

        [HttpGet("{roomID}")]
        [ProducesResponseType(typeof(RoomInfoResponse), 200)]
        public async Task<IActionResult> GetRoomInfo([FromRoute] RoomInfoRequest roomInfoRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetRoomInfo by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => roomsDAL.GetRoomInfo(roomInfoRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetRoomInfo by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpGet("roomlist")]
        [ProducesResponseType(typeof(PagingResult<RoomInfoResponse>), 200)]
        public async Task<IActionResult> GetRoomList([FromQuery] RoomListRequest roomListRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetRoomList by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => roomsDAL.GetRoomList(roomListRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetRoomList by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(ApiOkResponse<RoomAddResponse>), statusCode: 200)]
        public async Task<IActionResult> InsertRoom([FromBody]RoomAddRequest roomAddRequest)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => roomsDAL.InsertRoom(roomAddRequest));
        }



        [HttpPatch]
        [ProducesResponseType(type: typeof(ApiOkResponse<RoomUpdateResponse>), statusCode: 200)]
        public async Task<IActionResult> UpdateRoom([FromBody]RoomUpdateRequest model)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => roomsDAL.UpdateRoom(model));
        }



        [HttpGet("backup")]
        [ProducesResponseType(typeof(PagingResult<RoomAddResponse>), 200)]
        public async Task<IActionResult> BackupRoom()
        {
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => roomsDAL.BackupRoom());
        }

    }
}
