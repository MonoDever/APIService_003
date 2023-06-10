using System;
namespace APIService_003.DTO.Models.OptionalModels
{
	public class MailSettingModel
	{
        public string? mail { get; set; }
        public string? displayName { get; set; }
        public string? password { get; set; }
        public string? host { get; set; }
        public int port { get; set; }
    }
}

