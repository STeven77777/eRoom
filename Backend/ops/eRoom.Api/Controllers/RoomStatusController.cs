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
    [Route("api/roomStatus")]
    [Produces("application/json")]
    [ApiController]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 400)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 404)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 500)]
    public class RoomStatusController : ControllerBase
    {
        private readonly IRoomStatusDAL roomStatusDAL;
        private readonly IMapper mapper;
        private ILogger logger;
        private string ModuleName = "eRoom.API";//System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public RoomStatusController(IRoomStatusDAL _roomStatusDAL, ILogger<RoomStatusController> _logger, IMapper _mapper)
        {
            roomStatusDAL = _roomStatusDAL;
            logger = _logger;
            mapper = _mapper;
        }

        [HttpGet("{roomStatusID}")]
        [ProducesResponseType(typeof(RoomStatusInfoResponse), 200)]
        public async Task<IActionResult> GetRoomStatusInfo([FromRoute] RoomStatusInfoRequest roomStatusInfoRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetRoomStatusInfo by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => roomStatusDAL.GetRoomStatusInfo(roomStatusInfoRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetRoomStatusInfo by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpGet("roomStatuslist")]
        [ProducesResponseType(typeof(PagingResult<RoomStatusInfoResponse>), 200)]
        public async Task<IActionResult> GetRoomStatusList([FromQuery] RoomStatusListRequest roomStatusListRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetRoomStatusList by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => roomStatusDAL.GetRoomStatusList(roomStatusListRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetRoomStatusList by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(ApiOkResponse<RoomStatusAddResponse>), statusCode: 200)]
        public async Task<IActionResult> InsertRoomStatus([FromBody]RoomStatusAddRequest roomStatusAddRequest)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => roomStatusDAL.InsertRoomStatus(roomStatusAddRequest));
        }



        [HttpPatch]
        [ProducesResponseType(type: typeof(ApiOkResponse<RoomStatusUpdateResponse>), statusCode: 200)]
        public async Task<IActionResult> UpdateRoomStatus([FromBody]RoomStatusUpdateRequest model)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => roomStatusDAL.UpdateRoomStatus(model));
        }


    }
}
