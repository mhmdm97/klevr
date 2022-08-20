using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using klevr.Core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace klevr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserLimitController : ControllerBase
    {

        private readonly ILogger<UserLimitController> _logger;
        private readonly IUserLimitRepository _userLimitRepository;

        public UserLimitController(ILogger<UserLimitController> logger, IUserLimitRepository userLimitRepository)
        {
            _logger = logger;
            _userLimitRepository = userLimitRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var userLimits = _userLimitRepository.GetUserLimits();
                if (userLimits != null)
                    return StatusCode(StatusCodes.Status200OK, userLimits);
            }
            catch(Exception) { }
            return StatusCode(StatusCodes.Status500InternalServerError);

        }
        [HttpGet("CheckTransferValidity")]
        public IActionResult CheckTransferValidity(Guid userId, Guid accountId, double transactionAmount)
        {
            try
            {
                var validity = _userLimitRepository.CheckTransactionValidity(userId, accountId, transactionAmount);
                if (validity.Success)
                    return StatusCode(StatusCodes.Status200OK, validity.Message);
                return StatusCode(StatusCodes.Status406NotAcceptable, validity.Message);
            }
            catch (Exception) { }
            return StatusCode(StatusCodes.Status500InternalServerError);

        }
    }
}
