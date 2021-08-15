using System;
using System.Threading.Tasks;
using APSystem.Models.Sms;
using APSystem.Services.Sms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace APSystem.Core.Controllers.Sms
{
    // [Route("api/v1/Sms")]
    // [ApiController]
    // public class SmsController : BaseController<SmsController>
    // {
    //     public ISmsService _smsService;
    //     public SmsController(ILogger<SmsController> logger, ISmsService smsService) : base(logger)
    //     {
    //         _smsService = smsService;
    //     }

    //      [HttpPost]
    //     [Route("Sms")]
    //     public async Task<ActionResult<SmSResponse>> Login([FromBody] SmsRequest request)
    //     {
    //         var response = await _smsService.SendSmsAsync(request);
    //         return response;
    //     }
    // }
}
