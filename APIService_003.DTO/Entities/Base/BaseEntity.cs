using System;
namespace APIService_003.DTO.Entities.Base
{
	public class BaseEntity : ErrorEntity
	{
		public bool status => statusMessage == "Success" ? true : false;
		public string? statusMessage { get; set; } = null;
		public BaseEntity()
		{

		}
	}
}

