using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIService_003.BSL.IService;
using APIService_003.DTO.Entities;
using APIService_003.DTO.Entities.Base;
using APIService_003.DTO.Models;
using APIService_003.DTO.Models.OptionalModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIService_003.Controllers
{

    [Authorize]
    [ApiController]
    public class UserController : Controller
    {
        public readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]/register")]
        public IActionResult RegisterUser([FromBody] UserModel userModel)
        {
            IActionResult response = Conflict();
            ResultEntity resultEntity = new ResultEntity();
            resultEntity = _userService.RegisterUser(userModel);
            if (resultEntity.status == true)
            {
                return response = Ok(resultEntity);
            }
            else
            {
                return response = Conflict(resultEntity);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]/login")]
        public IActionResult Login([FromBody] UserModel userModel)
        {
            IActionResult response = Unauthorized();
            UserEntity userEntity = new UserEntity();
            userEntity = _userService.Login(userModel);
            if (userEntity.status == true)
            {
                return response = Ok(userEntity);
            }
            else
            {
                return response = Conflict(userEntity);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]/forgotpassword")]
        public async Task<IActionResult> Forgotpassword(MailRequestModel mailRequestModel)
        {
            IActionResult response = Unauthorized();
            ResultEntity resultEntity = new ResultEntity();

            resultEntity = await _userService.ForgotPassword(mailRequestModel);

            if (resultEntity.status == true)
            {
                return response = Ok(resultEntity);
            }
            else
            {
                return response = Conflict(resultEntity);
            }
        }
        [AllowAnonymous]
        [HttpPatch]
        [Route("api/[controller]/resetpassword")]
        public IActionResult ResetPassword(UserModel userModel)
        {
            IActionResult response = Conflict();
            ResultEntity resultEntity = new ResultEntity();

            resultEntity = _userService.ResetPassword(userModel);

            if (resultEntity.status == true)
            {
                return response = Ok(resultEntity);
            }
            else
            {
                return response = Conflict(resultEntity);
            }
        }

        [HttpPost]
        [Route("api/[controller]/getuserinformation")]
        public IActionResult GetUserInfomation([FromBody] UserInformationModel userInformationModel)
        {
            IActionResult response = Unauthorized();
            UserInfomationEntity userInformationEntity = new UserInfomationEntity();

            userInformationEntity = _userService.GetUserInfomation(userInformationModel);

            if (userInformationEntity.status)
            {
                return response = Ok(userInformationEntity);
            }
            else
            {
                return response = Conflict(userInformationEntity);
            }
        }
    }
}

