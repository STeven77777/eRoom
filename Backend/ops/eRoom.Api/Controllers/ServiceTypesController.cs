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
    [Route("api/serviceTypes")]
    [Produces("application/json")]
    [ApiController]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 400)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 404)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 500)]
    public class ServiceTypesController : ControllerBase
    {
        private readonly IServiceTypesDAL serviceTypesDAL;
        private readonly IMapper mapper;
        private ILogger logger;
        private string ModuleName = "eRoom.API";//System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public ServiceTypesController(IServiceTypesDAL _serviceTypesDAL, ILogger<ServiceTypesController> _logger, IMapper _mapper)
        {
            serviceTypesDAL = _serviceTypesDAL;
            logger = _logger;
            mapper = _mapper;
        }

        [HttpGet("{serviceTypeID}")]
        [ProducesResponseType(typeof(ServiceTypeInfoResponse), 200)]
        public async Task<IActionResult> GetServiceTypeInfo([FromRoute] ServiceTypeInfoRequest serviceTypeInfoRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetServiceTypeInfo by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => serviceTypesDAL.GetServiceTypeInfo(serviceTypeInfoRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetServiceTypeInfo by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpGet("serviceTypelist")]
        [ProducesResponseType(typeof(PagingResult<ServiceTypeInfoResponse>), 200)]
        public async Task<IActionResult> GetServiceTypeList([FromQuery] ServiceTypeListRequest serviceTypeListRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetServiceTypeList by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => serviceTypesDAL.GetServiceTypeList(serviceTypeListRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetServiceTypeList by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(ApiOkResponse<ServiceTypeAddResponse>), statusCode: 200)]
        public async Task<IActionResult> InsertServiceType([FromBody]ServiceTypeAddRequest serviceTypeAddRequest)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => serviceTypesDAL.InsertServiceType(serviceTypeAddRequest));
        }



        [HttpPatch]
        [ProducesResponseType(type: typeof(ApiOkResponse<ServiceTypeUpdateResponse>), statusCode: 200)]
        public async Task<IActionResult> UpdateServiceType([FromBody]ServiceTypeUpdateRequest model)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => serviceTypesDAL.UpdateServiceType(model));
        }


    }
}
