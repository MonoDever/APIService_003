using System;
using System.Text.Json;
using APIService_003.BSL.BSLUtility;
using APIService_003.BSL.BSLUtility.Validation;
using APIService_003.BSL.IService;
using APIService_003.DAL.IData;
using APIService_003.DTO.Entities;
using APIService_003.DTO.Entities.Base;
using APIService_003.DTO.Models;
using APIService_003.DTO.Models.CommonModels;
using APIService_003.DTO.Models.OptionalModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace APIService_003.BSL.Service
{
    public class UserService : IUserService
    {
        public readonly IUserData _userData;
        private readonly IMailService _mailService;
        private IConfiguration _configuration;
        public GenerateToken _generateToken;
        private ILogService _logService;

        public UserService(IConfiguration configuration, IUserData userData, ILogData logData, IMailService mailService)
        {
            _userData = userData;
            _mailService = mailService;
            _configuration = configuration;
            _generateToken = new GenerateToken(_configuration);
            _logService = new LogService(logData);
        }

        public ResultEntity RegisterUser(UserModel userModel)
        {
            ResultEntity resultEntity = new ResultEntity();
            Enums.UserError enumError = Enums.UserError.None;
            try
            {
                #region Insert Log Activity on Request Action
                LogModel logModelRequest = new LogModel()
                {
                    serviceName = "RegisterUser",
                    action = "Request",
                    detail = JsonSerializer.Serialize(userModel)
                };
                _logService.InsertLogActivity(logModelRequest);
                #endregion Insert Log Activity on Request Action

                #region Validate
                var validUsername = false;
                var validPassword = false;
                var validFirstname = false;
                var validLastname = false;
                Task[] tasks = new Task[4];

                tasks[0] = Task.Run(() =>
                {
                    validUsername = UserValidation.UsernameValidate(userModel.username);
                    //Thread.Sleep(1000);
                });
                tasks[1] = Task.Run(() =>
                validPassword = UserValidation.PasswordValidate(userModel.password)
                );
                tasks[2] = Task.Run(() =>
                validFirstname = UserValidation.FirstnameValidate(userModel.firstname)
                );
                tasks[3] = Task.Run(() =>
                validLastname = UserValidation.LastnameValidate(userModel.lastname)
                );
                Task.WaitAll(tasks);

                if (!validUsername || !validPassword || !validFirstname || !validLastname)
                {
                    if (!validUsername)
                    {
                        enumError = Enums.UserError.Parameter_Username_is_Invalid;
                    }
                    else if (!validPassword)
                    {
                        enumError = Enums.UserError.Parameter_Password_is_Invalid;
                    }
                    else if (!validFirstname)
                    {
                        enumError = Enums.UserError.Parameter_Firstname_is_Invalid;
                    }
                    else if (!validLastname)
                    {
                        enumError = Enums.UserError.Parameter_Lastname_is_Invalid;
                    }
                    throw new Exception(enumError.ToString());
                }
                #endregion Validate

                #region Verify a username already
                var userAlready = _userData.GetUserByUsername(userModel.username);
                if (userAlready.user != null)
                {
                    enumError = Enums.UserError.Username_is_already;
                    throw new Exception(enumError.ToString());
                }
                #endregion Verify a username already

                #region Setup Password and saltvalue ********** not use
                //var newSaltValue = CommonFunctions.GenerateSalt();
                //var newPassword = $"{userModel.password}{newSaltValue}";
                //var userAnotherModel = new UserAnotherModel();
                //userAnotherModel.newPassword = newPassword;
                //userAnotherModel.newSaltValue = newSaltValue;
                #endregion Setup Password and saltvalue

                resultEntity = _userData.RegisterUser(userModel);

                ResultHandle.SuccessHandle(resultEntity);

                #region Insert Log Activity on Response Action
                LogModel logModelResponse = new LogModel()
                {
                    serviceName = "RegisterUser",
                    action = "Response",
                    detail = JsonSerializer.Serialize(resultEntity)
                };
                _logService.InsertLogActivity(logModelResponse);
                #endregion Insert Log Activity on Response Action.
            }
            catch (Exception ex)
            {
                enumError = enumError == Enums.UserError.None ? Enums.UserError.UserData_is_occur_Error : enumError;
                ResultHandle.ExceptionHandle(ex, resultEntity, enumError);

                #region Insert Log Activity on ErrorResponse Action
                LogModel logModelResponse = new LogModel()
                {
                    serviceName = "RegisterUser",
                    action = "ErrorResponse",
                    detail = JsonSerializer.Serialize(resultEntity)
                };
                _logService.InsertLogActivity(logModelResponse);
                #endregion Insert Log Activity on ErrorResponse Action

                return resultEntity;
            }

            return resultEntity;
        }

        public UserEntity Login(UserModel userModel)
        {
            UserEntity userEntity = new UserEntity();
            Enums.UserError enumError = Enums.UserError.None;
            try
            {
                #region Insert log Activity on Request Action
                LogModel logModelRequest = new LogModel()
                {
                    serviceName = "Login",
                    action = "Request",
                    detail = JsonSerializer.Serialize(userModel)
                };
                _logService.InsertLogActivity(logModelRequest);
                #endregion Insert log Activity on Request Action

                #region Validate
                var validUsername = UserValidation.UsernameValidate(userModel.username);
                var validPassword = UserValidation.PasswordValidate(userModel.password);
                if (!validUsername || !validPassword)
                {
                    if (!validUsername)
                    {
                        enumError = Enums.UserError.Parameter_username_or_password_is_invalid;
                    }
                    else if (!validPassword)
                    {
                        enumError = Enums.UserError.Parameter_username_or_password_is_invalid;
                    }

                    throw new Exception(enumError.ToString());
                }
                #endregion Validate

                userEntity = _userData.Login(userModel);
                if (userEntity != null && userEntity.user != null)
                {
                    var stringToken = _generateToken.CreateToken(userEntity);
                    userEntity.user.auth = stringToken;
                }
                ResultHandle.SuccessHandle(userEntity);

                #region Insert Log Activity on Response Action
                LogModel logModelResponse = new LogModel()
                {
                    serviceName = "RegisterUser",
                    action = "Response",
                    detail = JsonSerializer.Serialize(userEntity)
                };
                _logService.InsertLogActivity(logModelResponse);
                #endregion Insert Log Activity on Response Action
            }
            catch (Exception ex)
            {
                enumError = enumError == Enums.UserError.None ? Enums.UserError.UserData_is_occur_Error : enumError;
                ResultHandle.ExceptionHandle(ex, userEntity, enumError);

                #region Insert Log Activity on ErrorResponse Action
                LogModel logModelResponse = new LogModel()
                {
                    serviceName = "RegisterUser",
                    action = "ErrorResponse",
                    detail = JsonSerializer.Serialize(userEntity)
                };
                _logService.InsertLogActivity(logModelResponse);
                #endregion Insert Log Activity on ErrorResponse Action

                return userEntity;
            }

            return userEntity;
        }
        public async Task<ResultEntity> ForgotPassword(MailRequestModel mailRequestModel)
        {
            ResultEntity resultEntity = new ResultEntity();
            Enums.UserError enumError = Enums.UserError.None;
            try
            {

                #region Validate email address
                var validEmail = UserValidation.UsernameValidate(mailRequestModel.toEmail);
                if (!validEmail)
                {
                    enumError = Enums.UserError.Parameter_Username_is_Invalid;
                    throw new Exception(enumError.ToString());
                }
                #endregion Validate email address

                #region Verify a username already
                var userAlready = _userData.GetUserByUsername(mailRequestModel.toEmail);
                if (userAlready.user == null)
                {
                    enumError = Enums.UserError.Username_not_already;
                    throw new Exception(enumError.ToString());
                }
                #endregion Verify a username already

                mailRequestModel.link = $"http://localhost:3000/resetpassword?email={mailRequestModel.toEmail}&activationToken=todo";

                resultEntity = await _mailService.SendEmailTemplateAsync(mailRequestModel);

                ResultHandle.SuccessHandle(resultEntity);
            }
            catch (Exception ex)
            {
                enumError = enumError == Enums.UserError.None ? Enums.UserError.UserData_is_occur_Error : enumError;
                ResultHandle.ExceptionHandle(ex, resultEntity, enumError);
            }

            return resultEntity;
        }

        public ResultEntity ResetPassword(UserModel userModel)
        {
            ResultEntity resultEntity = new ResultEntity();
            Enums.UserError enumError = Enums.UserError.None;
            try
            {
                #region Validate
                var validPassword = UserValidation.PasswordValidate(userModel.password);
                if (!validPassword)
                {
                    enumError = Enums.UserError.Parameter_Password_is_Invalid;
                    throw new Exception(enumError.ToString());
                }
                #endregion Validate

                resultEntity = _userData.ResetPassword(userModel);
                ResultHandle.SuccessHandle(resultEntity);
            }
            catch (Exception ex)
            {
                enumError = enumError == Enums.UserError.None ? Enums.UserError.UserData_is_occur_Error : enumError;
                ResultHandle.ExceptionHandle(ex, resultEntity, enumError);
            }
            return resultEntity;

        }
        public UserInfomationEntity GetUserInfomation(UserInformationModel userInformationModel)
        {
            UserInfomationEntity userInfomationEntity = new UserInfomationEntity();
            Enums.UserError enumError = Enums.UserError.None;
            try
            {
                userInfomationEntity = _userData.GetUserInformation(userInformationModel);
                ResultHandle.SuccessHandle(userInfomationEntity);
            }
            catch (Exception ex)
            {
                enumError = enumError == Enums.UserError.None ? Enums.UserError.UserData_is_occur_Error : enumError;
                ResultHandle.ExceptionHandle(ex, userInfomationEntity, enumError);
            }

            return userInfomationEntity;
        }
    }
}

