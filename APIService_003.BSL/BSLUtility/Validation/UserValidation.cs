using System;
using System.Text.RegularExpressions;

namespace APIService_003.BSL.BSLUtility.Validation
{
    public static class UserValidation
    {
        public static bool UsernameValidate(string? username)
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
        public static bool PasswordValidate(string? password)
        {
            string pattern = @"^\$2[ayb]\$.{56}$";
            Regex regex = new Regex(pattern);
            if (!string.IsNullOrEmpty(password))
            {
                return regex.Match(password).Success;
            }
            else
            {
                return false;
            }
        }
        public static bool FirstnameValidate(string? firsname)
        {
            string pattern = @"^[A-Za-z]{1,50}$";
            Regex regex = new Regex(pattern);
            if (!string.IsNullOrEmpty(firsname))
            {
                return regex.Match(firsname).Success;
            }
            else
            {
                return false;
            }
        }
        public static bool LastnameValidate(string? lastname)
        {
            string pattern = @"^[A-Za-z]{1,50}$";
            Regex regex = new Regex(pattern);
            if (!string.IsNullOrEmpty(lastname))
            {
                return regex.Match(lastname).Success;
            }
            else
            {
                return false;
            }
        }
    }
}

