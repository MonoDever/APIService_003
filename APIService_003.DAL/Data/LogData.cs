using System;
using System.Data;
using System.Data.SqlClient;
using APIService_003.DAL.Connection;
using APIService_003.DAL.IData;
using APIService_003.DTO.Entities.Base;
using APIService_003.DTO.Models;
using Microsoft.Extensions.Configuration;

namespace APIService_003.DAL.Data
{
	public class LogData : ILogData
	{
        private ExecuteCommand _executeCommand;
        private string SP_INSERT_LOG_ACTIVITY = "SP_Insert_Log_Activity";
		public LogData(IConfiguration configuration)
		{
            ExecuteCommand executeCommand = new ExecuteCommand(configuration);
            _executeCommand = executeCommand;
        }

        public ResultEntity InsertLogActivity(LogModel logModel)
        {
            ResultEntity resultEntity = new ResultEntity();
            logModel.createdBy = "System";

            SqlParameter[] sqlParameter = new SqlParameter[5];
            sqlParameter[0] = new SqlParameter { ParameterName = "@ServiceName", SqlDbType = SqlDbType.NVarChar, Value = string.IsNullOrEmpty(logModel.serviceName) ? DBNull.Value : logModel.serviceName };
            sqlParameter[1] = new SqlParameter { ParameterName = "@Action", SqlDbType = SqlDbType.NVarChar, Value = string.IsNullOrEmpty(logModel.action) ? DBNull.Value : logModel.action };
            sqlParameter[2] = new SqlParameter { ParameterName = "@Detail", SqlDbType = SqlDbType.NVarChar, Value = string.IsNullOrEmpty(logModel.detail) ? DBNull.Value : logModel.detail };
            sqlParameter[3] = new SqlParameter { ParameterName = "@CreatedBy", SqlDbType = SqlDbType.NVarChar, Value = string.IsNullOrEmpty(logModel.createdBy) ? DBNull.Value : logModel.createdBy };
            sqlParameter[4] = new SqlParameter { ParameterName = "@CreatedDate", SqlDbType = SqlDbType.DateTime, Value = DBNull.Value };

            var result = _executeCommand.CreateUpdateDataByProcedure(SP_INSERT_LOG_ACTIVITY,sqlParameter);
            resultEntity.statusMessage = result;
            return resultEntity;
        }
    }
}