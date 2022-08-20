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
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserLimitController> _logger;
        private readonly IUserRepository _userRepository;

        public UserController(ILogger<UserLimitController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

       
        [HttpGet("GetActiveAccountUsers")]
        public IActionResult GetActiveAccountUsers()
        {
            try
            {
                var res = _userRepository.GetUsersWithActiveAccounts();
                if (res != null)
                    return StatusCode(StatusCodes.Status200OK, res);
            }
            catch (Exception) { }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
