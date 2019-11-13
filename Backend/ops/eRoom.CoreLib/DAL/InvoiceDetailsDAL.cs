using eRoom.CoreLib.Common;
using eRoom.Shared.CoreLib.Models.Request;
using eRoom.Shared.CoreLib.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRoom.CoreLib.DAL
{
    public interface IInvoiceDetailsDAL
    {
        Task<(DefaultMetaResult h, InvoiceDetailInfoResponse r)> GetInvoiceDetailInfo(InvoiceDetailInfoRequest _param);
        Task<(DefaultMetaResult h, PagingResult<InvoiceDetailInfoResponse> r)> GetInvoiceDetailList(InvoiceDetailListRequest _param);
        Task<(DefaultMetaResult, InvoiceDetailAddResponse)> InsertInvoiceDetail(InvoiceDetailAddRequest invoiceDetailAddRequest);
        Task<(DefaultMetaResult, InvoiceDetailUpdateResponse)> UpdateInvoiceDetail(InvoiceDetailUpdateRequest invoiceDetailAddRequest);
    }
    public class InvoiceDetailsDAL : BaseDAL, IInvoiceDetailsDAL
    {
        public InvoiceDetailsDAL(IConfiguration _configuration, ILogger<InvoiceDetailsDAL> logger) : base(_configuration, logger)
        {
        }
    
        public async Task<(DefaultMetaResult h, InvoiceDetailInfoResponse r)> GetInvoiceDetailInfo(InvoiceDetailInfoRequest _param)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, InvoiceDetailInfoResponse>(SP.GetInvoiceDetailInfo, _param);
        }

        public async Task<(DefaultMetaResult h, PagingResult<InvoiceDetailInfoResponse> r)> GetInvoiceDetailList(InvoiceDetailListRequest _param)
        {
            return await ExecSPReturnListWithAsync<DefaultMetaResult, InvoiceDetailInfoResponse>(SP.GetInvoiceDetailList, _param);
        }

        public async Task<(DefaultMetaResult, InvoiceDetailAddResponse)> InsertInvoiceDetail(InvoiceDetailAddRequest invoiceDetailAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, InvoiceDetailAddResponse>(SP.InsertInvoiceDetail, invoiceDetailAddRequest);
        }

        public async Task<(DefaultMetaResult, InvoiceDetailUpdateResponse)> UpdateInvoiceDetail(InvoiceDetailUpdateRequest invoiceDetailAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, InvoiceDetailUpdateResponse>(SP.UpdateInvoiceDetail, invoiceDetailAddRequest);
        }
    }
}
