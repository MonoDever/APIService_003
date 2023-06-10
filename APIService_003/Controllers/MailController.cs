using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIService_003.BSL.IService;
using APIService_003.DTO.Entities.Base;
using APIService_003.DTO.Models.OptionalModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIService_003.Controllers
{
    [Authorize]
    [ApiController]
    public class MailController : Controller
    {
        private readonly IMailService _mailService;
        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]/sendmail")]
        public async Task<IActionResult> SendMail([FromBody] MailRequestModel mailRequestModel)
        {
            ResultEntity resultEntity = new ResultEntity();
            IActionResult response = Conflict();

            resultEntity = await _mailService.SendEmailTemplateAsync(mailRequestModel);

            if (resultEntity.status == true)
            {
                return response = Ok(resultEntity);
            }
            else
            {
                return response = Conflict(resultEntity);
            }
        }
    }
}

