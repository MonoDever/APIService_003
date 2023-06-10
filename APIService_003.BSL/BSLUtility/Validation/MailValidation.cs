using System;
using System.Text.RegularExpressions;

namespace APIService_003.BSL.BSLUtility.Validation
{
	public static class MailValidation
	{
        public static bool EmailValidate(string? username)
        {
            string pattern = @"^(([^<>()[\]\\.,;:\s@""]+(\.[^<>()[\]\\.,;:\s@""]+)*)|.("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";
            Regex regex = new Regex(pattern);
            if (!string.IsNullOrEmpty(username))
            {
                return regex.Match(username).Success;
            }
            else
            {
                return false;
            }
        }
    }
}

