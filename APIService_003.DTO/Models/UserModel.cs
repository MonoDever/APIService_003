using System;
namespace APIService_003.DTO.Models
{
	public class UserModel
	{
		public UserModel()
		{
		}

		public string? userId { get; set; }
		public string? username { get; set; }
		public string? password { get; set; }
		public string? firstname { get; set; }
		public string? lastname { get; set; }
        public string? email { get; set; }
        public string? auth { get; set; }
    }
}

