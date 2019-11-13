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
    [Route("api/waterUsings")]
    [Produces("application/json")]
    [ApiController]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 400)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 404)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 500)]
    public class WaterUsingsController : ControllerBase
    {
        private readonly IWaterUsingsDAL waterUsingsDAL;
        private readonly IMapper mapper;
        private ILogger logger;
        private string ModuleName = "eRoom.API";//System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public WaterUsingsController(IWaterUsingsDAL _waterUsingsDAL, ILogger<WaterUsingsController> _logger, IMapper _mapper)
        {
            waterUsingsDAL = _waterUsingsDAL;
            logger = _logger;
            mapper = _mapper;
        }

        [HttpGet("{waterUsingID}")]
        [ProducesResponseType(typeof(WaterUsingInfoResponse), 200)]
        public async Task<IActionResult> GetWaterUsingInfo([FromRoute] WaterUsingInfoRequest waterUsingInfoRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetWaterUsingInfo by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => waterUsingsDAL.GetWaterUsingInfo(waterUsingInfoRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetWaterUsingInfo by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpGet("waterUsinglist")]
        [ProducesResponseType(typeof(PagingResult<WaterUsingInfoResponse>), 200)]
        public async Task<IActionResult> GetWaterUsingList([FromQuery] WaterUsingListRequest waterUsingListRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetWaterUsingList by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => waterUsingsDAL.GetWaterUsingList(waterUsingListRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetWaterUsingList by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(ApiOkResponse<WaterUsingAddResponse>), statusCode: 200)]
        public async Task<IActionResult> InsertWaterUsing([FromBody]WaterUsingAddRequest waterUsingAddRequest)
        {
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => waterUsingsDAL.InsertWaterUsing(waterUsingAddRequest));
        }



        [HttpPatch]
        [ProducesResponseType(type: typeof(ApiOkResponse<WaterUsingUpdateResponse>), statusCode: 200)]
        public async Task<IActionResult> UpdateWaterUsing([FromBody]WaterUsingUpdateRequest model)
        {
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => waterUsingsDAL.UpdateWaterUsing(model));
        }


    }
}
