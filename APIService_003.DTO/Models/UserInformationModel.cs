using System;
namespace APIService_003.DTO.Models
{
	public class UserInformationModel
	{
		public UserInformationModel()
		{

		}
		public string? username { get; set; }
		public string? firstname { get; set; }
		public string? lastname { get; set; }
		public string? address { get; set; }
		public string? email { get; set; }
		public string? phone { get; set; }
	}
}