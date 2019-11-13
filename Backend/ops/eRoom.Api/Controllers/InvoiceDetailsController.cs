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
    [Route("api/invoiceDetails")]
    [Produces("application/json")]
    [ApiController]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 400)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 404)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 500)]
    public class InvoiceDetailsController : ControllerBase
    {
        private readonly IInvoiceDetailsDAL invoiceDetailsDAL;
        private readonly IMapper mapper;
        private ILogger logger;
        private string ModuleName = "eRoom.API";//System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public InvoiceDetailsController(IInvoiceDetailsDAL _invoiceDetailsDAL, ILogger<InvoiceDetailsController> _logger, IMapper _mapper)
        {
            invoiceDetailsDAL = _invoiceDetailsDAL;
            logger = _logger;
            mapper = _mapper;
        }

        [HttpGet("{invoiceDetailID}")]
        [ProducesResponseType(typeof(InvoiceDetailInfoResponse), 200)]
        public async Task<IActionResult> GetInvoiceDetailInfo([FromRoute] InvoiceDetailInfoRequest invoiceDetailInfoRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetInvoiceDetailInfo by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => invoiceDetailsDAL.GetInvoiceDetailInfo(invoiceDetailInfoRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetInvoiceDetailInfo by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpGet("invoiceDetaillist")]
        [ProducesResponseType(typeof(PagingResult<InvoiceDetailInfoResponse>), 200)]
        public async Task<IActionResult> GetInvoiceDetailList([FromQuery] InvoiceDetailListRequest invoiceDetailListRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetInvoiceDetailList by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => invoiceDetailsDAL.GetInvoiceDetailList(invoiceDetailListRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetInvoiceDetailList by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(ApiOkResponse<InvoiceDetailAddResponse>), statusCode: 200)]
        public async Task<IActionResult> InsertInvoiceDetail([FromBody]InvoiceDetailAddRequest invoiceDetailAddRequest)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => invoiceDetailsDAL.InsertInvoiceDetail(invoiceDetailAddRequest));
        }



        [HttpPatch]
        [ProducesResponseType(type: typeof(ApiOkResponse<InvoiceDetailUpdateResponse>), statusCode: 200)]
        public async Task<IActionResult> UpdateInvoiceDetail([FromBody]InvoiceDetailUpdateRequest model)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => invoiceDetailsDAL.UpdateInvoiceDetail(model));
        }


    }
}
