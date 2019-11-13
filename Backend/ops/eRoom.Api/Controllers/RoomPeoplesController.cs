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
    [Route("api/roomPeoples")]
    [Produces("application/json")]
    [ApiController]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 400)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 404)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 500)]
    public class RoomPeoplesController : ControllerBase
    {
        private readonly IRoomPeoplesDAL roomPeoplesDAL;
        private readonly IMapper mapper;
        private ILogger logger;
        private string ModuleName = "eRoom.API";//System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public RoomPeoplesController(IRoomPeoplesDAL _roomPeoplesDAL, ILogger<RoomPeoplesController> _logger, IMapper _mapper)
        {
            roomPeoplesDAL = _roomPeoplesDAL;
            logger = _logger;
            mapper = _mapper;
        }

        [HttpGet("{roomPeopleID}")]
        [ProducesResponseType(typeof(RoomPeopleInfoResponse), 200)]
        public async Task<IActionResult> GetRoomPeopleInfo([FromRoute] RoomPeopleInfoRequest roomPeopleInfoRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetRoomPeopleInfo by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => roomPeoplesDAL.GetRoomPeopleInfo(roomPeopleInfoRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetRoomPeopleInfo by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpGet("roomPeoplelist")]
        [ProducesResponseType(typeof(PagingResult<RoomPeopleInfoResponse>), 200)]
        public async Task<IActionResult> GetRoomPeopleList([FromQuery] RoomPeopleListRequest roomPeopleListRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetRoomPeopleList by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => roomPeoplesDAL.GetRoomPeopleList(roomPeopleListRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetRoomPeopleList by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(ApiOkResponse<RoomPeopleAddResponse>), statusCode: 200)]
        public async Task<IActionResult> InsertRoomPeople([FromBody]RoomPeopleAddRequest roomPeopleAddRequest)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => roomPeoplesDAL.InsertRoomPeople(roomPeopleAddRequest));
        }



        [HttpPatch]
        [ProducesResponseType(type: typeof(ApiOkResponse<RoomPeopleUpdateResponse>), statusCode: 200)]
        public async Task<IActionResult> UpdateRoomPeople([FromBody]RoomPeopleUpdateRequest model)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => roomPeoplesDAL.UpdateRoomPeople(model));
        }


    }
}
