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
    [Route("api/roomTypes")]
    [Produces("application/json")]
    [ApiController]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 400)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 404)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 500)]
    public class RoomTypesController : ControllerBase
    {
        private readonly IRoomTypesDAL roomTypesDAL;
        private readonly IMapper mapper;
        private ILogger logger;
        private string ModuleName = "eRoom.API";//System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public RoomTypesController(IRoomTypesDAL _roomTypesDAL, ILogger<RoomTypesController> _logger, IMapper _mapper)
        {
            roomTypesDAL = _roomTypesDAL;
            logger = _logger;
            mapper = _mapper;
        }

        [HttpGet("{roomTypeID}")]
        [ProducesResponseType(typeof(RoomTypeInfoResponse), 200)]
        public async Task<IActionResult> GetRoomTypeInfo([FromRoute] RoomTypeInfoRequest roomTypeInfoRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetRoomTypeInfo by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => roomTypesDAL.GetRoomTypeInfo(roomTypeInfoRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetRoomTypeInfo by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpGet("roomTypelist")]
        [ProducesResponseType(typeof(PagingResult<RoomTypeInfoResponse>), 200)]
        public async Task<IActionResult> GetRoomTypeList([FromQuery] RoomTypeListRequest roomTypeListRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetRoomTypeList by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => roomTypesDAL.GetRoomTypeList(roomTypeListRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetRoomTypeList by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(ApiOkResponse<RoomTypeAddResponse>), statusCode: 200)]
        public async Task<IActionResult> InsertRoomType([FromBody]RoomTypeAddRequest roomTypeAddRequest)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => roomTypesDAL.InsertRoomType(roomTypeAddRequest));
        }



        [HttpPatch]
        [ProducesResponseType(type: typeof(ApiOkResponse<RoomTypeUpdateResponse>), statusCode: 200)]
        public async Task<IActionResult> UpdateRoomType([FromBody]RoomTypeUpdateRequest model)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => roomTypesDAL.UpdateRoomType(model));
        }


    }
}
