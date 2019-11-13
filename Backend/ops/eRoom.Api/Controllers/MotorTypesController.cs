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
    [Route("api/motorTypes")]
    [Produces("application/json")]
    [ApiController]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 400)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 404)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 500)]
    public class MotorTypesController : ControllerBase
    {
        private readonly IMotorTypesDAL motorTypesDAL;
        private readonly IMapper mapper;
        private ILogger logger;
        private string ModuleName = "eRoom.API";//System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public MotorTypesController(IMotorTypesDAL _motorTypesDAL, ILogger<MotorTypesController> _logger, IMapper _mapper)
        {
            motorTypesDAL = _motorTypesDAL;
            logger = _logger;
            mapper = _mapper;
        }

        [HttpGet("{motorTypeID}")]
        [ProducesResponseType(typeof(MotorTypeInfoResponse), 200)]
        public async Task<IActionResult> GetMotorTypeInfo([FromRoute] MotorTypeInfoRequest motorTypeInfoRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetMotorTypeInfo by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => motorTypesDAL.GetMotorTypeInfo(motorTypeInfoRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetMotorTypeInfo by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpGet("motorTypelist")]
        [ProducesResponseType(typeof(PagingResult<MotorTypeInfoResponse>), 200)]
        public async Task<IActionResult> GetMotorTypeList([FromQuery] MotorTypeListRequest motorTypeListRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetMotorTypeList by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => motorTypesDAL.GetMotorTypeList(motorTypeListRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetMotorTypeList by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(ApiOkResponse<MotorTypeAddResponse>), statusCode: 200)]
        public async Task<IActionResult> InsertMotorType([FromBody]MotorTypeAddRequest motorTypeAddRequest)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => motorTypesDAL.InsertMotorType(motorTypeAddRequest));
        }



        [HttpPatch]
        [ProducesResponseType(type: typeof(ApiOkResponse<MotorTypeUpdateResponse>), statusCode: 200)]
        public async Task<IActionResult> UpdateMotorType([FromBody]MotorTypeUpdateRequest model)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => motorTypesDAL.UpdateMotorType(model));
        }


    }
}
