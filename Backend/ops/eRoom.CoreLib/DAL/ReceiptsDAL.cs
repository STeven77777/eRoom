using eRoom.CoreLib.Common;
using eRoom.Shared.CoreLib.Models.Request;
using eRoom.Shared.CoreLib.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRoom.CoreLib.DAL
{
    public interface IReceiptsDAL
    {
        Task<(DefaultMetaResult h, ReceiptInfoResponse r)> GetReceiptInfo(ReceiptInfoRequest _param);
        Task<(DefaultMetaResult h, PagingResult<ReceiptInfoResponse> r)> GetReceiptList(ReceiptListRequest _param);
        Task<(DefaultMetaResult, ReceiptAddResponse)> InsertReceipt(ReceiptAddRequest receiptAddRequest);
        Task<(DefaultMetaResult, ReceiptUpdateResponse)> UpdateReceipt(ReceiptUpdateRequest receiptAddRequest);
    }
    public class ReceiptsDAL : BaseDAL, IReceiptsDAL
    {
        public ReceiptsDAL(IConfiguration _configuration, ILogger<ReceiptsDAL> logger) : base(_configuration, logger)
        {
        }
    
        public async Task<(DefaultMetaResult h, ReceiptInfoResponse r)> GetReceiptInfo(ReceiptInfoRequest _param)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, ReceiptInfoResponse>(SP.GetReceiptInfo, _param);
        }

        public async Task<(DefaultMetaResult h, PagingResult<ReceiptInfoResponse> r)> GetReceiptList(ReceiptListRequest _param)
        {
            return await ExecSPReturnListWithAsync<DefaultMetaResult, ReceiptInfoResponse>(SP.GetReceiptList, _param);
        }

        public async Task<(DefaultMetaResult, ReceiptAddResponse)> InsertReceipt(ReceiptAddRequest receiptAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, ReceiptAddResponse>(SP.InsertReceipt, receiptAddRequest);
        }

        public async Task<(DefaultMetaResult, ReceiptUpdateResponse)> UpdateReceipt(ReceiptUpdateRequest receiptAddRequest)
        {
            return await ExecSPForItemResultAsync<DefaultMetaResult, ReceiptUpdateResponse>(SP.UpdateReceipt, receiptAddRequest);
        }
    }
}
