using System;
using APIService_003.DTO.Entities.Base;
using APIService_003.DTO.Models;

namespace APIService_003.DTO.Entities
{
	public class UserEntity : ResultEntity
	{
		public UserModel? user { get; set; }
	}
	public class UserEntities : ResultEntity
	{
		public List<UserModel>? users { get; set; }
	}
}

