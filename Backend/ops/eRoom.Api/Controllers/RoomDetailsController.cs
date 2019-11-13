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
    [Route("api/roomDetails")]
    [Produces("application/json")]
    [ApiController]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 400)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 404)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 500)]
    public class RoomDetailsController : ControllerBase
    {
        private readonly IRoomDetailsDAL roomDetailsDAL;
        private readonly IMapper mapper;
        private ILogger logger;
        private string ModuleName = "eRoom.API";//System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public RoomDetailsController(IRoomDetailsDAL _roomDetailsDAL, ILogger<RoomDetailsController> _logger, IMapper _mapper)
        {
            roomDetailsDAL = _roomDetailsDAL;
            logger = _logger;
            mapper = _mapper;
        }

        [HttpGet("{roomDetailID}")]
        [ProducesResponseType(typeof(RoomDetailInfoResponse), 200)]
        public async Task<IActionResult> GetRoomDetailInfo([FromRoute] RoomDetailInfoRequest roomDetailInfoRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetRoomDetailInfo by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => roomDetailsDAL.GetRoomDetailInfo(roomDetailInfoRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetRoomDetailInfo by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpGet("roomDetaillist")]
        [ProducesResponseType(typeof(PagingResult<RoomDetailInfoResponse>), 200)]
        public async Task<IActionResult> GetRoomDetailList([FromQuery] RoomDetailListRequest roomDetailListRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetRoomDetailList by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => roomDetailsDAL.GetRoomDetailList(roomDetailListRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetRoomDetailList by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(ApiOkResponse<RoomDetailAddResponse>), statusCode: 200)]
        public async Task<IActionResult> InsertRoomDetail([FromBody]RoomDetailAddRequest roomDetailAddRequest)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => roomDetailsDAL.InsertRoomDetail(roomDetailAddRequest));
        }



        [HttpPatch]
        [ProducesResponseType(type: typeof(ApiOkResponse<RoomDetailUpdateResponse>), statusCode: 200)]
        public async Task<IActionResult> UpdateRoomDetail([FromBody]RoomDetailUpdateRequest model)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => roomDetailsDAL.UpdateRoomDetail(model));
        }


    }
}
