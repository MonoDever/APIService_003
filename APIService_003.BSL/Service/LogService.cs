using System;
using APIService_003.BSL.IService;
using APIService_003.DAL.IData;
using APIService_003.DTO.Entities.Base;
using APIService_003.DTO.Models;

namespace APIService_003.BSL.Service
{
	public class LogService : ILogService
	{
        public readonly ILogData _logData;
		public LogService(ILogData logData)
		{
            _logData = logData;
		}

        public ResultEntity InsertLogActivity(LogModel logModel)
        {
            ResultEntity resultEntity = new ResultEntity();
            resultEntity = _logData.InsertLogActivity(logModel);
            return resultEntity;
        }
    }
}

