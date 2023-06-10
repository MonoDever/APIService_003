using System;
namespace APIService_003.DTO.Models
{
    public class LogModel
    {
        public string? serviceName { get; set; }
        public string? action { get; set; }
        public string? detail { get; set; }
        public string? createdBy { get; set; }
        public DateTime? createDate { get; set; }
        public LogModel()
        {

        }
    }
}

