using System;
using APIService_003.DTO.Entities.Base;
using APIService_003.DTO.Models;

namespace APIService_003.DAL.IData
{
	public interface ILogData
	{
		public ResultEntity InsertLogActivity(LogModel logModel);
	}
}

