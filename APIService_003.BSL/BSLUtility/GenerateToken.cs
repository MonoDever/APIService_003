using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APIService_003.DTO.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace APIService_003.BSL.BSLUtility
{
	public class GenerateToken
	{
        private IConfiguration _configuration;

        public GenerateToken(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string CreateToken(UserEntity userEntity)
		{
			string result = "";
			if (userEntity != null)
			{
				result = BuildToken(userEntity);
			}
			return result;
		}
		private string BuildToken(UserEntity userEntity)
		{
			var token = new JwtSecurityToken();
			try
			{
				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
				var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
				var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:Expires"]));

				var claims = new[]
				{
					//new Claim(JwtRegisteredClaimNames.Sub, userEntity.user.firstname),
					//new Claim(JwtRegisteredClaimNames.Email, userEntity.user.email),
					new Claim(JwtRegisteredClaimNames.Sub, userEntity.user.userId),
					new Claim(JwtRegisteredClaimNames.Email, userEntity.user.username),
					new Claim("ApiService003","Learn JWT By Self Practice"),
					new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
				};
                token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Issuer"],
                    claims,
                    expires: expires,
                    signingCredentials: creds
                    );
            }
            catch (Exception ex)
            {
				throw new Exception(ex.Message);
            }

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
	}
}

