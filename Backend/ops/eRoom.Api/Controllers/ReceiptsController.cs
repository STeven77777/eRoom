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
    [Route("api/receipts")]
    [Produces("application/json")]
    [ApiController]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 400)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 404)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 500)]
    public class ReceiptsController : ControllerBase
    {
        private readonly IReceiptsDAL receiptsDAL;
        private readonly IMapper mapper;
        private ILogger logger;
        private string ModuleName = "eRoom.API";//System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public ReceiptsController(IReceiptsDAL _receiptsDAL, ILogger<ReceiptsController> _logger, IMapper _mapper)
        {
            receiptsDAL = _receiptsDAL;
            logger = _logger;
            mapper = _mapper;
        }

        [HttpGet("{receiptID}")]
        [ProducesResponseType(typeof(ReceiptInfoResponse), 200)]
        public async Task<IActionResult> GetReceiptInfo([FromRoute] ReceiptInfoRequest receiptInfoRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetReceiptInfo by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => receiptsDAL.GetReceiptInfo(receiptInfoRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetReceiptInfo by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpGet("receiptlist")]
        [ProducesResponseType(typeof(PagingResult<ReceiptInfoResponse>), 200)]
        public async Task<IActionResult> GetReceiptList([FromQuery] ReceiptListRequest receiptListRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetReceiptList by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => receiptsDAL.GetReceiptList(receiptListRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetReceiptList by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(ApiOkResponse<ReceiptAddResponse>), statusCode: 200)]
        public async Task<IActionResult> InsertReceipt([FromBody]ReceiptAddRequest receiptAddRequest)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => receiptsDAL.InsertReceipt(receiptAddRequest));
        }



        [HttpPatch]
        [ProducesResponseType(type: typeof(ApiOkResponse<ReceiptUpdateResponse>), statusCode: 200)]
        public async Task<IActionResult> UpdateReceipt([FromBody]ReceiptUpdateRequest model)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => receiptsDAL.UpdateReceipt(model));
        }


    }
}
