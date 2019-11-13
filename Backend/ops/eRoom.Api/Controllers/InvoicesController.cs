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
    [Route("api/invoices")]
    [Produces("application/json")]
    [ApiController]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 400)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 404)]
    //[ProducesResponseType(typeof(ApiErrorRequestResponse), 500)]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoicesDAL invoicesDAL;
        private readonly IMapper mapper;
        private ILogger logger;
        private string ModuleName = "eRoom.API";//System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public InvoicesController(IInvoicesDAL _invoicesDAL, ILogger<InvoicesController> _logger, IMapper _mapper)
        {
            invoicesDAL = _invoicesDAL;
            logger = _logger;
            mapper = _mapper;
        }

        [HttpGet("{invoiceID}")]
        [ProducesResponseType(typeof(InvoiceInfoResponse), 200)]
        public async Task<IActionResult> GetInvoiceInfo([FromRoute] InvoiceInfoRequest invoiceInfoRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetInvoiceInfo by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => invoicesDAL.GetInvoiceInfo(invoiceInfoRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetInvoiceInfo by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpGet("invoicelist")]
        [ProducesResponseType(typeof(PagingResult<InvoiceInfoResponse>), 200)]
        public async Task<IActionResult> GetInvoiceList([FromQuery] InvoiceListRequest invoiceListRequest)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            string path = HttpContext == null ? "" : HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString.ToString();
            logger.LogInformation("[{ModuleName}] Executing GetInvoiceList by: {path}", ModuleName, path);
            var traceId = HttpContext == null ? "" : HttpContext.TraceIdentifier.Replace(":", "");
            var result = await WebApiWrapper.CallWithApiOkResponseAsync(x => invoicesDAL.GetInvoiceList(invoiceListRequest), traceId);
            watch.Stop();
            logger.LogInformation("[{ModuleName}] Executed GetInvoiceList by: {path} {statusCode} {result} in {elapseTime}ms"
                , ModuleName
                , path
                , result.StatusCode
                , JsonConvert.SerializeObject(result.Value)
                , watch.ElapsedMilliseconds.ToString()
                );
            return result;
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(ApiOkResponse<InvoiceAddResponse>), statusCode: 200)]
        public async Task<IActionResult> InsertInvoice([FromBody]InvoiceAddRequest invoiceAddRequest)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => invoicesDAL.InsertInvoice(invoiceAddRequest));
        }



        [HttpPatch]
        [ProducesResponseType(type: typeof(ApiOkResponse<InvoiceUpdateResponse>), statusCode: 200)]
        public async Task<IActionResult> UpdateInvoice([FromBody]InvoiceUpdateRequest model)
        {          
            return await WebApiWrapper.CallWithApiOkResponseAsync(x => invoicesDAL.UpdateInvoice(model));
        }


    }
}
