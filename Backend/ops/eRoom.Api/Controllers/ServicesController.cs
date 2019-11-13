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
    [Route("api/services")]
    [Produces("application/json")]
    [ApiController]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 400)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 404)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 500)]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesDAL servicesDAL;
        private readonly IMapper mapper;
        private ILogger logger;
        private string ModuleName = "eRoom.API";//System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public ServicesController(IServicesDAL _servicesDAL, ILogger<ServicesController> _logger, IMapper _mapper)
        {
            servicesDAL = _servicesDAL;
            logger = _logger;
            mapper = _mapper;
        }

        [HttpGet("{serviceID}")]
        [ProducesResponseType(typeof(ServiceInfoResponse), 200)]
        public async Task<IActionResult> GetServiceInfo([FromRoute] ServiceInfoRequest serviceInfoRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetServiceInfo by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => servicesDAL.GetServiceInfo(serviceInfoRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetServiceInfo by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpGet("servicelist")]
        [ProducesResponseType(typeof(PagingResult<ServiceInfoResponse>), 200)]
        public async Task<IActionResult> GetServiceList([FromQuery] ServiceListRequest serviceListRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetServiceList by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => servicesDAL.GetServiceList(serviceListRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetServiceList by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(ApiOkResponse<ServiceAddResponse>), statusCode: 200)]
        public async Task<IActionResult> InsertService([FromBody]ServiceAddRequest serviceAddRequest)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => servicesDAL.InsertService(serviceAddRequest));
        }



        [HttpPatch]
        [ProducesResponseType(type: typeof(ApiOkResponse<ServiceUpdateResponse>), statusCode: 200)]
        public async Task<IActionResult> UpdateService([FromBody]ServiceUpdateRequest model)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => servicesDAL.UpdateService(model));
        }


    }
}
