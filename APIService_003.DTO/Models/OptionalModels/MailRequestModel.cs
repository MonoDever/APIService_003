using System;
using Microsoft.AspNetCore.Http;

namespace APIService_003.DTO.Models.OptionalModels
{
	public class MailRequestModel
	{
		public MailRequestModel()
		{
		}
		public string? toEmail { get; set; }
		public string? subject { get; set; }
		public string? body { get; set; }
		public string? link { get; set; }
		public List<IFormFile>? attachments { get; set; }
	}
}

