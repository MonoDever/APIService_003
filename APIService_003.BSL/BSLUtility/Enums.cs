using System;
namespace APIService_003.BSL.BSLUtility
{
	public class Enums
	{
		public enum Status
		{
			None = 0,
			Success = 1,
			Unsuccess = 2
		}
		public enum UserError
		{
			None = 0000,
			Parameter_Username_is_Invalid = 0001,
            Parameter_Password_is_Invalid = 0002,
            Parameter_Firstname_is_Invalid = 0003,
            Parameter_Lastname_is_Invalid = 0004,
			Username_is_already = 0005,
			UserData_is_occur_Error = 0006,
			Parameter_username_or_password_is_invalid = 0007,
			Username_not_already = 0008,

		}
		public enum MailError
		{
			None = 0000,
			Parameter_EmailAddress_is_Invalid = 0001
		}
	}
}

