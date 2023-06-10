using System;
using System.Security.Cryptography;

namespace APIService_003.BSL.BSLUtility
{
	public class CommonFunctions
	{
		public CommonFunctions()
		{
		}
		public static string GenerateSalt()
		{
			var bytes = new byte[128 / 8];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(bytes);
            }

			return Convert.ToBase64String(bytes);
        }
	}
}

