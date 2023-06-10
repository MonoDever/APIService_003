using System;
using APIService_003.DTO.Entities;
using APIService_003.DTO.Entities.Base;
using APIService_003.DTO.Models;
using APIService_003.DTO.Models.CommonModels;

namespace APIService_003.DAL.IData
{
	public interface IUserData
	{
		public ResultEntity RegisterUser(UserModel userModel);
		public UserEntity GetUserByUsername(string username);
		public UserEntity Login(UserModel userModel);
		public ResultEntity ResetPassword(UserModel userModel);
		public UserInfomationEntity GetUserInformation(UserInformationModel userInformationModel);
	}
}