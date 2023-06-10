using System;
using APIService_003.DTO.Entities.Base;
using APIService_003.DTO.Models;

namespace APIService_003.DTO.Entities
{
    public class UserInfomationEntity : ResultEntity
    {
        public UserInformationModel? userInformation { get; set; }
    }
    public class UserInformationEntities : ResultEntity
    {
        public List<UserInformationModel>? userInfomations { get; set; }
    }
}

