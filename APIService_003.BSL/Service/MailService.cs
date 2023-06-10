using System;
using APIService_003.BSL.BSLUtility;
using APIService_003.BSL.BSLUtility.Validation;
using APIService_003.BSL.IService;
using APIService_003.DAL.IData;
using APIService_003.DTO.Entities.Base;
using APIService_003.DTO.Models.OptionalModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace APIService_003.BSL.Service
{
    public class MailService : IMailService
    {
        private readonly IMailData _mailData;
        public MailService(IMailData mailData)
        {
            _mailData = mailData;
        }

        public async Task<ResultEntity> SendEmailAsync(MailRequestModel mailRequestModel)
        {
            ResultEntity resultEntity = new ResultEntity();

            var newLine = Environment.NewLine;
            //Mock Value
            mailRequestModel.subject = "[Practice003]Reset password request";
            mailRequestModel.body = $"hello, {newLine}To into Practice003, your Id is : monorun {newLine}if you forgot your password";

            var mailSetting = _mailData.SettingEmailData();
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(mailSetting.mail);
            email.To.Add(MailboxAddress.Parse(mailRequestModel.toEmail));
            email.Subject = mailRequestModel.subject;

            var bodyBuilder = new BodyBuilder();
            if (mailRequestModel.attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequestModel.attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        bodyBuilder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            bodyBuilder.HtmlBody = mailRequestModel.body;
            email.Body = bodyBuilder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(mailSetting.host, mailSetting.port, SecureSocketOptions.StartTls);
            smtp.Authenticate(mailSetting.mail, mailSetting.password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);

            return resultEntity;
        }

        public async Task<ResultEntity> SendEmailTemplateAsync(MailRequestModel mailRequestModel)
        {
            ResultEntity resultEntity = new ResultEntity();
            Enums.MailError enumError = Enums.MailError.None;
            //Mock Value
            if (mailRequestModel.subject == null)
            {
                mailRequestModel.subject = "[Practice003]Reset password request";
            }
            if (mailRequestModel.link == null)
            {
                mailRequestModel.link = "http://localhost:3000/resetpasswords";
            }
            
            try
            {
                #region Validate MailRequest
                var validEmail = MailValidation.EmailValidate(mailRequestModel.toEmail);

                if (!validEmail)
                {
                    throw new Exception(Enums.MailError.Parameter_EmailAddress_is_Invalid.ToString());
                }

                #endregion Validate MailRequest

                var mailSetting = _mailData.SettingEmailData();
                string filePath = Directory.GetCurrentDirectory() + ".BSL/BSLUtility/EmailTemplates/ForgotPassword.html";
                StreamReader streamReader = new StreamReader(filePath);
                string mailText = streamReader.ReadToEnd();
                streamReader.Close();
                mailText = mailText.Replace("[username]", mailRequestModel.toEmail).Replace("[link]", mailRequestModel.link);//todo

                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(mailSetting.mail);
                email.To.Add(MailboxAddress.Parse(mailRequestModel.toEmail));
                email.Subject = mailRequestModel.subject;
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = mailText;
                email.Body = bodyBuilder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(mailSetting.host, mailSetting.port, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailSetting.mail, mailSetting.password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

                ResultHandle.SuccessHandle(resultEntity);
            }
            catch (Exception ex)
            {
                enumError = enumError == Enums.MailError.None ? Enums.MailError.Parameter_EmailAddress_is_Invalid : enumError;

                ResultHandle.ExceptionHandle(ex, resultEntity, enumError);
                return resultEntity;
            }

            return resultEntity;

        }
    }
}

