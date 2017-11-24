using Shared.Enums;
using Shared.JsonModels.ViewModels.ResponseModels;
using System.ComponentModel.DataAnnotations;

namespace Utils
{
    public static class PostRequestValidator
    {
        public static StatusResponse IsValidBody<T>(object body)
        {
            if (body == null)
                return new StatusResponse(true, (int)ErrorCodes.ErrorEnums.EMPTY_POST_BODY);

            if (!Validator.TryValidateObject((T)body, new ValidationContext((T)body), null, true))
                return new StatusResponse(true, (int)ErrorCodes.ErrorEnums.EMPTY_REQUIRED_FIELD);

            return null;
        }
    }
}
