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
    [Route("api/peoples")]
    [Produces("application/json")]
    [ApiController]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 400)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 404)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 500)]
    public class PeoplesController : ControllerBase
    {
        private readonly IPeoplesDAL peoplesDAL;
        private readonly IMapper mapper;
        private ILogger logger;
        private string ModuleName = "eRoom.API";//System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public PeoplesController(IPeoplesDAL _peoplesDAL, ILogger<PeoplesController> _logger, IMapper _mapper)
        {
            peoplesDAL = _peoplesDAL;
            logger = _logger;
            mapper = _mapper;
        }

        [HttpGet("{peopleID}")]
        [ProducesResponseType(typeof(PeopleInfoResponse), 200)]
        public async Task<IActionResult> GetPeopleInfo([FromRoute] PeopleInfoRequest peopleInfoRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetPeopleInfo by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => peoplesDAL.GetPeopleInfo(peopleInfoRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetPeopleInfo by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpGet("peoplelist")]
        [ProducesResponseType(typeof(PagingResult<PeopleInfoResponse>), 200)]
        public async Task<IActionResult> GetPeopleList([FromQuery] PeopleListRequest peopleListRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetPeopleList by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => peoplesDAL.GetPeopleList(peopleListRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetPeopleList by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(ApiOkResponse<PeopleAddResponse>), statusCode: 200)]
        public async Task<IActionResult> InsertPeople([FromBody]PeopleAddRequest peopleAddRequest)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => peoplesDAL.InsertPeople(peopleAddRequest));
        }



        [HttpPatch]
        [ProducesResponseType(type: typeof(ApiOkResponse<PeopleUpdateResponse>), statusCode: 200)]
        public async Task<IActionResult> UpdatePeople([FromBody]PeopleUpdateRequest model)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => peoplesDAL.UpdatePeople(model));
        }


    }
}
