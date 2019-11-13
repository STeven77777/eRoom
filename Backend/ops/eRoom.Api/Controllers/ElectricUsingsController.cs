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
    [Route("api/electricUsings")]
    [Produces("application/json")]
    [ApiController]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 400)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 404)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 500)]
    public class ElectricUsingsController : ControllerBase
    {
        private readonly IElectricUsingsDAL electricUsingsDAL;
        private readonly IMapper mapper;
        private ILogger logger;
        private string ModuleName = "eRoom.API";//System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public ElectricUsingsController(IElectricUsingsDAL _electricUsingsDAL, ILogger<ElectricUsingsController> _logger, IMapper _mapper)
        {
            electricUsingsDAL = _electricUsingsDAL;
            logger = _logger;
            mapper = _mapper;
        }

        [HttpGet("{electricUsingID}")]
        [ProducesResponseType(typeof(ElectricUsingInfoResponse), 200)]
        public async Task<IActionResult> GetElectricUsingInfo([FromRoute] ElectricUsingInfoRequest electricUsingInfoRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetElectricUsingInfo by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => electricUsingsDAL.GetElectricUsingInfo(electricUsingInfoRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetElectricUsingInfo by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpGet("electricUsinglist")]
        [ProducesResponseType(typeof(PagingResult<ElectricUsingInfoResponse>), 200)]
        public async Task<IActionResult> GetElectricUsingList([FromQuery] ElectricUsingListRequest electricUsingListRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetElectricUsingList by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => electricUsingsDAL.GetElectricUsingList(electricUsingListRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetElectricUsingList by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(ApiOkResponse<ElectricUsingAddResponse>), statusCode: 200)]
        public async Task<IActionResult> InsertElectricUsing([FromBody]ElectricUsingAddRequest electricUsingAddRequest)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => electricUsingsDAL.InsertElectricUsing(electricUsingAddRequest));
        }



        [HttpPatch]
        [ProducesResponseType(type: typeof(ApiOkResponse<ElectricUsingUpdateResponse>), statusCode: 200)]
        public async Task<IActionResult> UpdateElectricUsing([FromBody]ElectricUsingUpdateRequest model)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => electricUsingsDAL.UpdateElectricUsing(model));
        }


    }
}
