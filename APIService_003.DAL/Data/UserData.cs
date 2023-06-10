using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using APIService_003.DAL.Connection;
using APIService_003.DAL.IData;
using APIService_003.DTO.Entities;
using APIService_003.DTO.Entities.Base;
using APIService_003.DTO.Models;
using APIService_003.DTO.Models.CommonModels;
using Microsoft.Extensions.Configuration;

namespace APIService_003.DAL.Data
{
    public class UserData : IUserData
    {
        private ExecuteCommand _executeCommand;
        private string SP_INSERT_USER = "SP_InsertUser";
        private string SP_GETUSERBYUSERNAME = "SP_GetUserByUsername";
        private string SP_GETUSERLOGIN = "SP_GetUserLogin";
        private string SP_UPDATEPASSWORD = "SP_UpdatePassword";
        private string SP_GETUSERINFORMATION = "SP_GetUserInformation";
        public UserData(IConfiguration configuration)
        {
            ExecuteCommand executeCommand = new ExecuteCommand(configuration);
            _executeCommand = executeCommand;
        }

        public ResultEntity RegisterUser(UserModel userModel)
        {
            ResultEntity resultEntity = new ResultEntity();
            var createdBy = "System";
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];
                sqlParameters[0] = new SqlParameter { ParameterName = "@username", SqlDbType = SqlDbType.NVarChar, Value = string.IsNullOrEmpty(userModel.username) ? DBNull.Value : userModel.username };
                sqlParameters[1] = new SqlParameter { ParameterName = "@password", SqlDbType = SqlDbType.NVarChar, Value = string.IsNullOrEmpty(userModel.password) ? DBNull.Value : userModel.password };
                sqlParameters[2] = new SqlParameter { ParameterName = "@saltValue", SqlDbType = SqlDbType.NVarChar, Value = string.IsNullOrEmpty(null) ? DBNull.Value : DBNull.Value };
                sqlParameters[3] = new SqlParameter { ParameterName = "@createdby", SqlDbType = SqlDbType.NVarChar, Value = createdBy };
                sqlParameters[4] = new SqlParameter { ParameterName = "@createddate", SqlValue = SqlDbType.DateTime, Value = DBNull.Value };
                var result = _executeCommand.CreateUpdateDataByProcedure(SP_INSERT_USER, sqlParameters);

                resultEntity.statusMessage = result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return resultEntity;
        }

        public UserEntity GetUserByUsername(string username)
        {
            UserEntity userEntity = new UserEntity();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter { ParameterName = "@Username", SqlDbType = SqlDbType.NVarChar, Value = string.IsNullOrEmpty(username) ? DBNull.Value : username };
                var result = _executeCommand.ReadData<UserModel>(SP_GETUSERBYUSERNAME, sqlParameters);
                if (result != "")
                {
                    var users = JsonSerializer.Deserialize<List<UserModel>>(result);
                    userEntity.user = users?.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return userEntity;
        }

        public UserEntity Login(UserModel userModel)
        {
            UserEntity userEntity = new UserEntity();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter { ParameterName = "@username", SqlDbType = SqlDbType.NVarChar, Value = string.IsNullOrEmpty(userModel.username) ? DBNull.Value : userModel.username };
                sqlParameters[1] = new SqlParameter { ParameterName = "@password", SqlDbType = SqlDbType.NVarChar, Value = string.IsNullOrEmpty(userModel.password) ? DBNull.Value : userModel.password };
                var result = _executeCommand.ReadData<UserModel>(SP_GETUSERLOGIN, sqlParameters);
                if (result != "")
                {
                    var users = JsonSerializer.Deserialize<List<UserModel>>(result);
                    userEntity.user = users?.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return userEntity;
        }

        public ResultEntity ResetPassword(UserModel userModel)
        {
            ResultEntity resultEntity = new ResultEntity();
            var updatedby = "System";
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[4];
                sqlParameters[0] = new SqlParameter { ParameterName = "@username", SqlDbType = SqlDbType.NVarChar, Value = string.IsNullOrEmpty(userModel.username) ? DBNull.Value : userModel.username };
                sqlParameters[1] = new SqlParameter { ParameterName = "@password", SqlDbType = SqlDbType.NVarChar, Value = string.IsNullOrEmpty(userModel.password) ? DBNull.Value : userModel.password };
                sqlParameters[2] = new SqlParameter { ParameterName = "@updatedby", SqlDbType = SqlDbType.NVarChar, Value = updatedby };
                sqlParameters[3] = new SqlParameter { ParameterName = "@updateddate", SqlDbType = SqlDbType.DateTime, Value = DBNull.Value };
                var result = _executeCommand.CreateUpdateDataByProcedure(SP_UPDATEPASSWORD,sqlParameters);
                resultEntity.statusMessage = result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return resultEntity;
        }

        public UserInfomationEntity GetUserInformation(UserInformationModel userInformationModel)
        {
            var userInfomationEntity = new UserInfomationEntity();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter { ParameterName = "@username", SqlDbType = SqlDbType.NVarChar, Value = string.IsNullOrEmpty(userInformationModel.username) ? DBNull.Value : userInformationModel.username };
                var result = _executeCommand.ReadData<UserInformationModel>(SP_GETUSERINFORMATION, sqlParameters);
                if (result != "")
                {
                    var userInformations = JsonSerializer.Deserialize<List<UserInformationModel>>(result);
                    userInfomationEntity.userInformation = userInformations?.FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return userInfomationEntity;
        }
    }
}

