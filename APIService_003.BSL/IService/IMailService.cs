using System;
using APIService_003.DTO.Entities.Base;
using APIService_003.DTO.Models.OptionalModels;

namespace APIService_003.BSL.IService
{
	public interface IMailService
	{
		Task<ResultEntity> SendEmailAsync(MailRequestModel mailRequestModel);
		Task<ResultEntity> SendEmailTemplateAsync(MailRequestModel mailRequestModel);
	}
}

