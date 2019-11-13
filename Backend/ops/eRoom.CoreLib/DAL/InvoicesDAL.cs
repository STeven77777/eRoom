using eRoom.CoreLib.Common;
using eRoom.Shared.CoreLib.Models.Request;
using eRoom.Shared.CoreLib.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRoom.CoreLib.DAL
{
    public interface IInvoicesDAL
    {
        Task<(DefaultMetaResult h, InvoiceInfoResponse r)> GetInvoiceInfo(InvoiceInfoRequest _param);
        Task<(DefaultMetaResult h, PagingResult<InvoiceInfoResponse> r)> GetInvoiceList(InvoiceListRequest _param);
        Task<(DefaultMetaResult, InvoiceAddResponse)> InsertInvoice(InvoiceAddRequest invoiceAddRequest);
        Task<(DefaultMetaResult, InvoiceUpdateResponse)> UpdateInvoice(InvoiceUpdateRequest invoiceAddRequest);
    }
    public class InvoicesDAL : BaseDAL, IInvoicesDAL
    {
        public InvoicesDAL(IConfiguration _configuration, ILogger<InvoicesDAL> logger) : base(_configuration, logger)
        {
        }
    
        public async Task<(DefaultMetaResult h, InvoiceInfoResponse r)> GetInvoiceInfo(InvoiceInfoRequest _param)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, InvoiceInfoResponse>(SP.GetInvoiceInfo, _param);
        }

        public async Task<(DefaultMetaResult h, PagingResult<InvoiceInfoResponse> r)> GetInvoiceList(InvoiceListRequest _param)
        {
            return await ExecSPReturnListWithAsync<DefaultMetaResult, InvoiceInfoResponse>(SP.GetInvoiceList, _param);
        }

        public async Task<(DefaultMetaResult, InvoiceAddResponse)> InsertInvoice(InvoiceAddRequest invoiceAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, InvoiceAddResponse>(SP.InsertInvoice, invoiceAddRequest);
        }

        public async Task<(DefaultMetaResult, InvoiceUpdateResponse)> UpdateInvoice(InvoiceUpdateRequest invoiceAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, InvoiceUpdateResponse>(SP.UpdateInvoice, invoiceAddRequest);
        }
    }
}
