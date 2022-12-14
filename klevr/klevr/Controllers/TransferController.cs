using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using klevr.Core.Repository;
using klevr.Core.TransferParamModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace klevr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferController : ControllerBase
    {

        private readonly ILogger<UserLimitController> _logger;
        private readonly ITransferRepository _transferRepository;

        public TransferController(ILogger<UserLimitController> logger, ITransferRepository transferRepository)
        {
            _logger = logger;
            _transferRepository = transferRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TransferParamModel model)
        {
            try
            {
                var res = await _transferRepository.ExecuteNewTransferAsync(model.OriginAccountId, model.TargetAccountId, model.TransferAmount);
                if (res)
                    return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception) { }
            return StatusCode(StatusCodes.Status500InternalServerError);

        }
        [HttpGet("GetTransactionsOverPeriod")]
        public IActionResult GetTransactionsOverPeriod(DateTime start, DateTime end)
        {
            try
            {
                var res = _transferRepository.GetBatchOfTransfersOverPeriod(start, end);
                if(res != null)
                    return StatusCode(StatusCodes.Status200OK, res);
            }
            catch (Exception) { }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
