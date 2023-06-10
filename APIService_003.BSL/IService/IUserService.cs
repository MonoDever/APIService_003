using System;
using APIService_003.DTO.Entities;
using APIService_003.DTO.Entities.Base;
using APIService_003.DTO.Models;
using APIService_003.DTO.Models.OptionalModels;

namespace APIService_003.BSL.IService
{
	public interface IUserService
	{
		public ResultEntity RegisterUser(UserModel userModel);
		public UserEntity Login(UserModel userModel);
		public Task<ResultEntity> ForgotPassword(MailRequestModel mailRequestModel);
		public ResultEntity ResetPassword(UserModel userModel);
		public UserInfomationEntity GetUserInfomation(UserInformationModel userInformationModel);
	}
}