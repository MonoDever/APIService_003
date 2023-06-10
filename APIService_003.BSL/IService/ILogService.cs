using System;
using APIService_003.DTO.Entities.Base;
using APIService_003.DTO.Models;

namespace APIService_003.BSL.IService
{
	public interface ILogService
	{
		public ResultEntity InsertLogActivity(LogModel logModel);
	}
}