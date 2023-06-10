using System;
namespace APIService_003.DTO.Entities.Base
{
	public class ErrorEntity
	{
		public bool isError => errorCode == "0000" ? false : true;
		public string? errorCode { get; set; }
		public string? errorMessage { get; set; } = null;
		public ErrorEntity()
		{

		}
	}
}

