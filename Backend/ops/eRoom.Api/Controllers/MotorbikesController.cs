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
    [Route("api/motorbikes")]
    [Produces("application/json")]
    [ApiController]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 400)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 404)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 500)]
    public class MotorbikesController : ControllerBase
    {
        private readonly IMotorbikesDAL motorbikesDAL;
        private readonly IMapper mapper;
        private ILogger logger;
        private string ModuleName = "eRoom.API";//System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public MotorbikesController(IMotorbikesDAL _motorbikesDAL, ILogger<MotorbikesController> _logger, IMapper _mapper)
        {
            motorbikesDAL = _motorbikesDAL;
            logger = _logger;
            mapper = _mapper;
        }

        [HttpGet("{motorbikeID}")]
        [ProducesResponseType(typeof(MotorbikeInfoResponse), 200)]
        public async Task<IActionResult> GetMotorbikeInfo([FromRoute] MotorbikeInfoRequest motorbikeInfoRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetMotorbikeInfo by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => motorbikesDAL.GetMotorbikeInfo(motorbikeInfoRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetMotorbikeInfo by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpGet("motorbikelist")]
        [ProducesResponseType(typeof(PagingResult<MotorbikeInfoResponse>), 200)]
        public async Task<IActionResult> GetMotorbikeList([FromQuery] MotorbikeListRequest motorbikeListRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetMotorbikeList by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => motorbikesDAL.GetMotorbikeList(motorbikeListRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetMotorbikeList by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(ApiOkResponse<MotorbikeAddResponse>), statusCode: 200)]
        public async Task<IActionResult> InsertMotorbike([FromBody]MotorbikeAddRequest motorbikeAddRequest)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => motorbikesDAL.InsertMotorbike(motorbikeAddRequest));
        }



        [HttpPatch]
        [ProducesResponseType(type: typeof(ApiOkResponse<MotorbikeUpdateResponse>), statusCode: 200)]
        public async Task<IActionResult> UpdateMotorbike([FromBody]MotorbikeUpdateRequest model)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => motorbikesDAL.UpdateMotorbike(model));
        }


    }
}
