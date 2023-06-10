using System;
using APIService_003.DAL.IData;
using APIService_003.DTO.Models.OptionalModels;
using Microsoft.Extensions.Configuration;

namespace APIService_003.DAL.Data
{
	public class MailData : IMailData
	{
        private IConfiguration _configuration;
		public MailData(IConfiguration configuration)
		{
            _configuration = configuration;
		}

        public MailSettingModel SettingEmailData()
        {
            MailSettingModel mailSettingModel = new MailSettingModel()
            {
                mail = _configuration["MailSettings:Mail"],
                displayName = _configuration["MailSettings:DisplayName"],
                password = _configuration["MailSettings:Password"],
                host = _configuration["MailSettings:Host"],
                port = int.Parse(_configuration["MailSettings:Port"])
            };

            return mailSettingModel;
        }
    }
}

