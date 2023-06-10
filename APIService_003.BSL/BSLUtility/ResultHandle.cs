using System;
using APIService_003.DTO.Entities.Base;

namespace APIService_003.BSL.BSLUtility
{
	public static class ResultHandle
	{
		public static void SuccessHandle(ResultEntity resultEntity)
		{
			var num = (int)Enums.UserError.None;
			resultEntity.errorCode = num.ToString().PadLeft(4, '0');
			resultEntity.statusMessage = Enums.Status.Success.ToString();
		}
        public static void ExceptionHandle<T>(Exception ex, ResultEntity resultEntity,T value) where T : System.Enum
        {
            var enumValue = EnumNamedValues<T>();//convert generic type to dictionary enum type
			var num = enumValue.Where(v => v.Value == value.ToString()).ToList();
			resultEntity.errorCode = num[0].Key.ToString().PadLeft(4, '0');
			resultEntity.errorMessage = num[0].Value.Replace('_',' ');
			resultEntity.statusMessage = Enums.Status.Unsuccess.ToString();
        }
        public static Dictionary<int, string> EnumNamedValues<T>() where T : System.Enum
        {
            var result = new Dictionary<int, string>();
            var values = Enum.GetValues(typeof(T));

            foreach (int item in values)
                result.Add(item, Enum.GetName(typeof(T), item));
            return result;
        }
    }
}

